using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using POC.BLL.Models;

namespace POC.BLL.Services
{
  public class ConfigurationService<T> : IConfigurationService<T> where T: IConfigurationModel
  {
    public ConfigurationService()
    {
    }

    public async Task<T> GetSettingsAsync()
    {
      T obj;

      using (var fs = new FileStream(
      $"Config//{typeof(T).Name}.json", FileMode.Open))
      {
        obj = await JsonSerializer.DeserializeAsync<T>(fs);
      }

      return obj;
    }

    public async Task UpdateSettingsAsync(T model)
    {
      var options = new JsonSerializerOptions
      {
        WriteIndented = true
      };

      using (var fs = new FileStream(
        $"Config//{typeof(T).Name}.json", FileMode.Create))
      {
        await JsonSerializer.SerializeAsync<T>(fs, model, options);
      }
    }
  }
}