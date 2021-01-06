using System.Collections.Generic;
using System.Threading.Tasks;
using POC.BLL.Model;
using POC.DAL.Entities;

namespace POC.BLL.Services
{
  public interface IInstagramService
  {
    Task<IList<InstagramMedia>> GetStoriesAsync(string name);
    Task<IList<InstagramMedia>> GetStoriesAsync();
    Task WriteMessageAsync(string text);
    Task UpdateDbInstaDataAsync();
  }
}