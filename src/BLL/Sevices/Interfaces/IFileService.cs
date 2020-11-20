using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using POC.BLL.Models;

namespace POC.BLL.Services
{
  public interface IFileService
  {
    Task AddFile(IFormFile file, string folder);
    Task<List<FileData>> GetFiles(string folder);
    Task DeleteFile(string fileId);
  }
}
