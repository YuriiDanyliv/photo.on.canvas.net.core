using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using POC.BLL.Model;

namespace POC.BLL.Interfaces
{
  public interface IFileService
  {
    Task AddFile(IFormFile file, string folder);
    Task<List<FileData>> GetFiles(string folder);
    Task DeleteFile(string fileId);
  }
}
