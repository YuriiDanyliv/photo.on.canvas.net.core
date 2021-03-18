using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POC.BLL.Dto;
using POC.BLL.Models;
using POC.BLL.Services;
using System.Threading.Tasks;

namespace POC.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IEmailConfirmService _emailConfirmService;
        private readonly IConfigurationService<EmailServiceConfig> _configService;

        public AccountController(
          IAccountService accounService,
          IEmailConfirmService emailConfirmService,
          IConfigurationService<EmailServiceConfig> configService)
        {
            _configService = configService;
            _accountService = accounService;
            _emailConfirmService = emailConfirmService;
        }

        /// <summary>
        /// Get users with pagination info
        /// </summary>
        /// <param name="param">page size and page number</param>
        /// <returns></returns>
        [HttpGet("GetUsers")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<PagesListModel<UserDto>>> GetUsers([FromQuery] UserQueryParam param)
        {
            var result = await _accountService.GetUsersAsync(param);
            if (result == null || result.data == null || result.data.Length == 0) return NoContent();
            return Ok(result);
        }

        /// <summary>
        /// Delete user by ID
        /// </summary>
        /// <param name="userId"> user ID</param>
        /// <returns></returns>
        [HttpDelete("DeleteUser")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IdentityResult>> DeleteUser([FromForm] string userId)
        {
            if (userId == null) return BadRequest("Invalid user id");
            var result = await _accountService.DeleteUserAsync(userId);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return Ok(result);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user">user data</param>
        /// <returns></returns>
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<IdentityResult>> Register([FromForm] UserAuthDto user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            IdentityResult result = await _accountService.RegisterAsync(user);
            if (!result.Succeeded) BadRequest(result.Errors);

            var emailConfig = await _configService.GetSettingsAsync();
            if (emailConfig.EmailConfirmIsOn) return Ok("follow the link provided in the email");

            return Ok(result);
        }

        /// <summary>
        /// Confirm email address
        /// </summary>
        /// <param name="userId">user ID</param>
        /// <param name="token">token</param>
        /// <returns></returns>
        [HttpGet("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<ActionResult<IdentityResult>> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) return BadRequest("Error: user Id == null or token == null");

            var result = await _emailConfirmService.ConfirmEmailAsync(userId, token);
            if (result.Succeeded) return Ok("Email Confirmed");

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="user">user data</param>
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login([FromForm] UserAuthDto user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var emailConfig = await _configService.GetSettingsAsync();
            if (emailConfig.EmailConfirmIsOn)
            {
                var isEmailConfirmed = await _emailConfirmService.ValidateConfirmedEmailAsync(user);
                if (!isEmailConfirmed) return BadRequest(new { msg = "Email not confirmed" });
            }

            var result = await _accountService.LoginAsync(user);
            if (result.SignInResult.Succeeded) return Ok(result.Jwt);

            return BadRequest(result.SignInResult);
        }

        /// <summary>
        /// Check if user have an admin role
        /// </summary>
        /// <returns></returns>
        [HttpGet("IsAdmin")]
        public ActionResult<bool> IsAdmin()
        {
            return (User.IsInRole("admin"));
        }

        /// <summary>
        /// Check if user authenticated
        /// </summary>
        /// <returns></returns>
        [HttpGet("isAuthenticated")]
        [AllowAnonymous]
        public ActionResult<bool> isAuthenticated()
        {
            return Ok(User.Identity.IsAuthenticated);
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        [HttpPost("Logout")]
        public async Task<ActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return Ok();
        }
    }
}
