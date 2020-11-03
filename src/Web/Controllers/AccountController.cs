using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using POC.BLL.DTO;
using POC.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using POC.BLL.Interfaces;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using POC.BLL.Model;

namespace POC.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountController : Controller
  {
    private readonly ILogger<AccountController> _logger;
    private readonly IAccountService _accountService;
    private readonly IEmailConfirmService _emailConfirmService;
    private readonly IConfigurationService _configService;
    private readonly IMapper _mapper;

    public AccountController(
      ILogger<AccountController> logger,
      IAccountService accounService,
      IEmailConfirmService emailConfirmService,
      IConfigurationService configService,
      IMapper mapper)
    {
      _logger = logger;
      _accountService = accounService;
      _emailConfirmService = emailConfirmService;
      _configService = configService;
      _mapper = mapper;
    }

    [HttpGet("GetUsers")]
    public ActionResult<PagesVM<UserDTO>> GetUsers([FromQuery] UserQueryParam param)
    {
      var result = _mapper.Map<PagesVM<UserDTO>>(_accountService.GetUsers(param));

      return Ok(result);
    }

    [HttpPost("DeleteUser")]
    public async Task<ActionResult<IdentityResult>> DeleteUser([FromBody] string userId)
    {
      var result = await _accountService.DeleteUserAsync(userId);
      if (result.Succeeded) return Ok(result);

      return BadRequest(result.Errors);
    }

    [HttpPost("Register")]
    public async Task<ActionResult<IdentityResult>> Register([FromBody] RegisterViewModel model)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);

      var mappedModel = _mapper.Map<UserAuthDTO>(model);
      IdentityResult result = await _accountService.RegisterAsync(mappedModel);

      if (result.Succeeded)
      {
        if (_configService.GetEmailConfig().ServiceIsOn)
        {
          await _emailConfirmService.SendConfirmEmailAsync(mappedModel, Url);
          return Ok("follow the link provided in the email");
        }
        return Ok(result);
      }

      return BadRequest(result.Errors);
    }

    [HttpGet("ConfirmEmail")]
    [AllowAnonymous]
    public async Task<ActionResult<IdentityResult>> ConfirmEmail(string userId, string token)
    {
      if (userId == null || token == null) return BadRequest("Error: user Id == null or token == null");

      var result = await _emailConfirmService.ConfirmEmailAsync(userId, token);
      if (result.Succeeded) return Ok("Email Confirmed");

      return BadRequest(result.Errors);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<SignInResult>> Login([FromBody] LoginViewModel model)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      var mappedModel = _mapper.Map<UserAuthDTO>(model);

      if (_configService.GetEmailConfig().ServiceIsOn)
      {
        var isEmailConfirmed = await _emailConfirmService.ValidateConfirmedEmailAsync(mappedModel);
        if (!isEmailConfirmed) return BadRequest(new { msg = "Email not confirmed" });
      }

      var result = await _accountService.LoginAsync(mappedModel);
      if (result.Succeeded) return Ok(new { Status = "Logged In", Result = result });

      return BadRequest(result);
    }

    [HttpPost("Logout")]
    public async Task<ActionResult> Logout()
    {
      await _accountService.LogoutAsync();
      return Ok();
    }
  }
}
