using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using POC.BLL.Interfaces;
using POC.DAL.Entities;
using POC.DAL.Interfaces;

namespace POC.BLL.Services
{
  public class FileService : IFileService
  {
    public FileService()
    {
      
    }

    public async Task<ImageFileData> AddOrderedImage(IFormFile uploadedFile)
    {
      var folderName = Path.Combine("Resources", "Images");
      var fileName = uploadedFile.FileName + Guid.NewGuid().ToString();
      var path = Path.Combine(Directory.GetCurrentDirectory(), folderName, fileName);

      try
      {
        using (var fileStream = new FileStream(path, FileMode.Create))
        {
          await uploadedFile.CopyToAsync(fileStream);
        }

        return new ImageFileData() { Name = uploadedFile.FileName, Path = path };
      }
      catch (System.Exception)
      {
        throw;
      }
    }
  }
}