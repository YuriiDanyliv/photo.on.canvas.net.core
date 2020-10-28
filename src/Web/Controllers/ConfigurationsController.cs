using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using POC.BLL.Interfaces;
using POC.BLL.Models;

namespace POC.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ConfigurationController : Controller
  {
    public IConfigurationService _cfgService { get; set; }

    public ConfigurationController(IConfigurationService cfgService)
    {
      _cfgService = cfgService;
    }

    [HttpGet("GetEmailConfiguration")]
    public ActionResult<EmailServiceConfig> GetEmailConfig()
    {
      return Ok(_cfgService.GetEmailConfig());
    }

    [HttpPost("SetEmailConfiguration")]
    public ActionResult SetEmailConfig([FromBody] EmailServiceConfig model)
    {
      _cfgService.SetEmailConfig(model);
      return Ok();
    }
  }
}