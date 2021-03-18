using System.Collections.Generic;
using System.Threading.Tasks;
using POC.BLL.Dto;

namespace POC.BLL.Services
{
    /// <summary>
    /// Interface for work with instagram api
    /// </summary>
    public interface IInstagramService
    {
        /// <summary>
        /// Returns stories by its name
        /// </summary>
        /// <param name="name">Name of stories highlights</param>
        /// <returns>List of stories</returns> 
        Task<IList<InstagramMediaDto>> GetStoriesAsync(string name);

        /// <summary>
        /// Returns all stories from database
        /// </summary>
        /// <returns>List of stories</returns>
        Task<IList<InstagramMediaDto>> GetStoriesAsync();

        /// <summary>
        /// Sends message for added instagram account
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        Task WriteMessageAsync(string text);

        /// <summary>
        ///  Updates all instagram stories in database
        /// </summary>
        /// <returns></returns>
        Task UpdateDbInstaDataAsync();
    }
}