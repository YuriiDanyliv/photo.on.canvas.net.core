using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using POC.BLL.DTO;
using POC.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using POC.BLL.Models;
using POC.BLL.Services;

namespace POC.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class AccountController : Controller
  {
    private readonly ILogger<AccountController> _logger;
    private readonly IAccountService _accountService;
    private readonly IEmailConfirmService _emailConfirmService;
    private readonly IMapper _mapper;
    private readonly IConfigurationService<EmailServiceConfig> _configService;

    public AccountController(
      ILogger<AccountController> logger,
      IAccountService accounService,
      IEmailConfirmService emailConfirmService,
      IConfigurationService<EmailServiceConfig> configService,
      IMapper mapper)
    {
      _configService = configService;
      _logger = logger;
      _accountService = accounService;
      _emailConfirmService = emailConfirmService;
      _mapper = mapper;
    }

    [HttpGet("GetUsers")]
    [Authorize(Roles = "admin")]
    public ActionResult<PagesVM<UserDTO>> GetUsers([FromQuery] UserQueryParam param)
    {
      var result = _mapper.Map<PagesVM<UserDTO>>(_accountService.GetUsers(param));

      return Ok(result);
    }

    [HttpDelete("DeleteUser")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<IdentityResult>> DeleteUser([FromBody] string userId)
    {
      var result = await _accountService.DeleteUserAsync(userId);
      if (result.Succeeded) return Ok(result);
      
      return BadRequest(result.Errors);
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<ActionResult<IdentityResult>> Register([FromBody] RegisterViewModel model)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);

      var mappedModel = _mapper.Map<UserAuthDTO>(model);
      IdentityResult result = await _accountService.RegisterAsync(mappedModel);

      if (result.Succeeded)
      {
        var emailConfig = await _configService.GetSettingsAsync();
        if (emailConfig.ServiceIsOn)
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
    [AllowAnonymous]
    public async Task<ActionResult<string>> Login([FromBody] LoginViewModel model)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      var mappedModel = _mapper.Map<UserAuthDTO>(model);

      var emailConfig = await _configService.GetSettingsAsync();
      if (emailConfig.ServiceIsOn)
      {
        var isEmailConfirmed = await _emailConfirmService.ValidateConfirmedEmailAsync(mappedModel);
        if (!isEmailConfirmed) return BadRequest(new { msg = "Email not confirmed" });
      }

      var result = await _accountService.LoginAsync(mappedModel);
      if (result.SignInResult.Succeeded) return Ok(result.Jwt);

      return BadRequest(result.SignInResult);
    }

    [HttpGet("IsAdmin")]
    public ActionResult<bool> IsAdmin()
    {
      return (User.IsInRole("admin") ? Ok(true) : Ok(false));
    }

    [HttpGet("isAuthenticated")]
    [AllowAnonymous]
    public ActionResult<bool> isAuthenticated()
    {
      return (User.Identity.IsAuthenticated ? Ok(true) : Ok(false));
    }

    [HttpGet("GetUserData")]
    public ActionResult<object> GetUserData()
    {
      var data = new 
      {
        username = User.Identity.Name,
      };

      return Ok(data);
    }

    [HttpPost("Logout")]
    public async Task<ActionResult> Logout()
    {
      await _accountService.LogoutAsync();
      return Ok();
    }
  }
}
