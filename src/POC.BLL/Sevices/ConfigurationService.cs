using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using POC.BLL.Interfaces;
using POC.BLL.Models;

namespace POC.BLL.Services
{
  public class ConfigurationService : IConfigurationService
  {
    public IConfiguration _config { get; set; }

    public ConfigurationService(IConfiguration config)
    {
      _config = config;
    }

    public EmailServiceConfig GetEmailConfig()
    {
      var result = GetSettingsAsync<EmailServiceConfig>().Result;
      return (result);
    }

    public void SetEmailConfig(EmailServiceConfig cfgModel)
    {
      UpdateSettings<EmailServiceConfig>(cfgModel);
    }


    private async Task<T> GetSettingsAsync<T>()
    {
      T obj;

      using (var fs = new FileStream(
      $"Config//{typeof(T).Name}.json", FileMode.Open))
      {
        obj = await JsonSerializer.DeserializeAsync<T>(fs);
      }

      return obj;
    }

    private void UpdateSettings<T>(T model)
    {
      var options = new JsonSerializerOptions
      {
        WriteIndented = true
      };

      using (var fs = new FileStream(
        $"Config//{typeof(T).Name}.json", FileMode.Create))
      {
        JsonSerializer.SerializeAsync<T>(fs, model, options);
      }
    }
  }
}