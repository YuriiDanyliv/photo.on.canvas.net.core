using System.Collections.Generic;
using System.Threading.Tasks;
using POC.BLL.Model;

namespace POC.BLL.Services
{
  public interface IInstagramService
  {
    Task<List<InstagramMediaModel>> GetStoriesByNameAsync(string storyName);
    Task WriteMessageAsync(string text);
  }
}