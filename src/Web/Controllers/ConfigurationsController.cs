using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POC.BLL.Models;
using POC.BLL.Services;

namespace POC.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : Controller
    {
        private readonly IConfigurationService<EmailServiceConfig> _emailConfig;
        private readonly IConfigurationService<InstagramServiceConfig> _instaConfig;

        public ConfigurationController(
          IConfigurationService<EmailServiceConfig> emailConfig,
          IConfigurationService<InstagramServiceConfig> instaConfig)
        {
            _emailConfig = emailConfig;
            _instaConfig = instaConfig;
        }
        
        /// <summary>
        /// Returns email services settings
        /// </summary>
        /// <returns>Email config model</returns>
        [HttpGet("GetEmailConfiguration")]
        public async Task<ActionResult<EmailServiceConfig>> GetEmailConfig()
        {
            var result = await _emailConfig.GetSettingsAsync();
            if (result == null) return NoContent();
            return Ok(result);
        }

        /// <summary>
        /// Set email services settings
        /// </summary>
        /// <param name="model">Email config model</param>
        /// <returns></returns>
        [HttpPost("SetEmailConfiguration")]
        public async Task<ActionResult> SetEmailConfig([FromForm] EmailServiceConfig model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _emailConfig.UpdateSettingsAsync(model);
            return Ok("Email settings updated");
        }

        /// <summary>
        /// Returns instagram services settings
        /// </summary>
        /// <returns>Instagram config model</returns>
        [HttpGet("GetInstagramConfiguration")]
        public async Task<ActionResult<EmailServiceConfig>> GetInstagramConfig()
        {
            var result = await _instaConfig.GetSettingsAsync();
            if (result == null) return NoContent();
            return Ok(result);
        }

        /// <summary>
        /// Set instagram services settings
        /// </summary>
        /// <param name="model">Instagram config model</param>
        /// <returns></returns>
        [HttpPost("SetInstagramConfiguration")]
        public async Task<ActionResult> SetInstagramConfig([FromForm] InstagramServiceConfig model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _instaConfig.UpdateSettingsAsync(model);
            return Ok("Instagram settings updated");
        }
    }
}