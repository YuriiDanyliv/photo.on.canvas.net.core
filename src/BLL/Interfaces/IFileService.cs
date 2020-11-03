using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using POC.DAL.Entities;

namespace POC.BLL.Interfaces
{
  public interface IFileService
  {
    Task<ImageFileData> AddOrderedImage(IFormFile file);
  }
}
