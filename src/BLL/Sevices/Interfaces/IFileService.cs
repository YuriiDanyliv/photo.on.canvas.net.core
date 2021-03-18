using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using POC.BLL.Models;

namespace POC.BLL.Services
{
    public interface IFileService
    {
        Task AddFile(IFormFile file, string folder);

        Task AddFile(string folder, string category, string contentType, byte[] bytes);

        Task<List<FileData>> GetFilesAsync(string folder);

        Task DeleteFile(string fileId);

    }
}
