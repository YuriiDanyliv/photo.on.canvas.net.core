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

    [HttpGet("GetEmailConfiguration")]
    public async Task<ActionResult<EmailServiceConfig>> GetEmailConfig()
    {
      return Ok(await _emailConfig.GetSettingsAsync());
    }

    [HttpPost("SetEmailConfiguration")]
    public async Task<ActionResult> SetEmailConfig([FromBody] EmailServiceConfig model)
    {
      await _emailConfig.UpdateSettingsAsync(model);
      return Ok();
    }

    [HttpGet("GetInstagramConfiguration")]
    public async Task<ActionResult<EmailServiceConfig>> GetInstagramConfig()
    {
      return Ok(await _instaConfig.GetSettingsAsync());
    }

    [HttpPost("SetInstagramConfiguration")]
    public async Task<ActionResult> SetInstagramConfig([FromForm] InstagramServiceConfig model)
    {
      await _instaConfig.UpdateSettingsAsync(model);
      return Ok();
    }
  }
}