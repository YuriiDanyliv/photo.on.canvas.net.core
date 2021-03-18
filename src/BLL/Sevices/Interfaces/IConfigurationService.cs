using System.Threading.Tasks;
using POC.BLL.Models;

namespace POC.BLL.Services
{
    public interface IConfigurationService<T> where T : IConfigurationModel
    {
        /// <summary>
        /// Get settings
        /// </summary>
        /// <returns></returns>
        Task<T> GetSettingsAsync();

        /// <summary>
        /// Update settings
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task UpdateSettingsAsync(T model);
    }
}