using System.Threading.Tasks;
using POC.BLL.Models;

namespace POC.BLL.Services
{
  public interface IConfigurationService<T> where T: IConfigurationModel
  {
    Task<T> GetSettingsAsync();
    Task UpdateSettingsAsync(T model);
  }
}