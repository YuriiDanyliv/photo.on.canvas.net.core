using System.Threading.Tasks;

namespace POC.BLL.Services
{
  public interface IInstagramService
  {
    Task GetStoriesAsync();
    Task WriteMessageAsync(string text);
  }
}