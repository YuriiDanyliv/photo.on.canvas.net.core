using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using POC.BLL.DTO;
using POC.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using POC.BLL.Interfaces;

namespace POC.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountController : Controller
  {
    private readonly ILogger<AccountController> _logger;
    private readonly IAccountService _accountService;
    private readonly IEmailConfirmService _emailConfirmService;
    private readonly IMapper _mapper;

    public AccountController(
      ILogger<AccountController> logger,
      IAccountService accounService,
      IEmailConfirmService emailConfirmService,
      IMapper mapper)
    {
      _logger = logger;
      _accountService = accounService;
      _emailConfirmService = emailConfirmService;
      _mapper = mapper;
    }

    [HttpPost("GetUsers")]
    public System.Linq.IQueryable<UserDTO> GetUsers()
    {
      var result = _accountService.GetUsers();

      _logger.LogInformation("Get users action executed");
      return(result);
    }

    [HttpPost("DeleteUser")]
    public async Task<IActionResult> DeleteUser([FromBody] string userId)
    {
      var result = await _accountService.DeleteUserAsync(userId);
      if (result.Succeeded) return Ok(result);

      return BadRequest(result.Errors);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);

      var mappedModel = _mapper.Map<UserAuthDTO>(model);
      IdentityResult result = await _accountService.RegisterAsync(mappedModel);

      if (result.Succeeded)
      {
        //await _emailConfirmService.SendConfirmEmailAsync(userModel, Url);
        return Ok("follow the link provided in the email");
      }

      return BadRequest(result.Errors);
    }

    [HttpGet("ConfirmEmail")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
      if (userId == null || token == null) return BadRequest("Error: user Id == null or token == null");

      var result = await _emailConfirmService.ConfirmEmailAsync(userId, token);
      if (result.Succeeded) return Ok("Email Confirmed");

      return BadRequest(result.Errors);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      var mappedModel = _mapper.Map<UserAuthDTO>(model);

      //var isEmailConfirmed = await _emailConfirmService.ValidateConfirmedEmailAsync(mappedModel);
      //if (!isEmailConfirmed) return BadRequest(new { msg = "Email not confirmed" });

      var result = await _accountService.LoginAsync(mappedModel);
      if (result.Succeeded) return Ok(new { Status = "Logged In", Result = result });

      return BadRequest(result);
    }

    [HttpPost("Logout")]
    public async Task Logout() => await _accountService.LogoutAsync();
  }
}