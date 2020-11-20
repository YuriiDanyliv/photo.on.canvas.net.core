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
    public IConfigurationService<EmailServiceConfig> _emailConfig { get; set; }

    public ConfigurationController(
      IConfigurationService<EmailServiceConfig> emailConfig)
    {
      _emailConfig = emailConfig;
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
  }
}