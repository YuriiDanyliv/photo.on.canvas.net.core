using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POC.BLL.Models;
using POC.DAL.Entities;
using POC.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace POC.BLL.Services
{
    public class FileService : IFileService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<FileService> _logger;

        public FileService(
          IUnitOfWork unitOfWork,
          ILogger<FileService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task AddFile(IFormFile file, string folder)
        {
            var fileName = file.FileName;
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", folder);
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            var path = Path.Combine(folderPath, fileName);
            if (File.Exists(path)) _logger.LogWarning("File with same name already exist");

            try
            {
                _unitOfWork.File.Create(new FileEntity()
                {
                    FileName = fileName,
                    Folder = folder,
                });
                await _unitOfWork.SaveAsync();

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex, "Add file Error");
            }
        }

        /// <inheritdoc/>
        public async Task AddFile(string folder, string category, string contentType, byte[] bytes)
        {
            var fileName = Guid.NewGuid().ToString();
            var path = GetPath(folder, fileName, contentType);

            try
            {
                _unitOfWork.File.Create(new FileEntity()
                {
                    FileName = fileName,
                    Folder = folder,
                    Category = category,
                    ContentType = contentType
                });
                await _unitOfWork.SaveAsync();

                File.WriteAllBytes(path, bytes);
            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex, "Add file Error");
            }
        }

        /// <inheritdoc/>
        public async Task DeleteFile(string fileId)
        {
            var file = await _unitOfWork.File.FindByIdAsync(fileId);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", file.Folder, file.FileName);

            File.Delete(path);
            _unitOfWork.File.Delete(file);
            await _unitOfWork.SaveAsync();
        }

        /// <inheritdoc/>
        public async Task<List<FileData>> GetFilesAsync(string folder)
        {
            var filesList = new List<FileData>();
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", folder);

            var filesData = await _unitOfWork.File
                .FindByCondition(file => file.Folder == folder)
                .ToListAsync();

            foreach (var item in filesData)
            {
                byte[] file = File.ReadAllBytes(Path.Combine(folderPath, item.FileName));

                filesList.Add(new FileData()
                {
                    Id = item.Id,
                    FileName = item.FileName,
                    File = file,
                });
            }

            return filesList;
        }

        private string GetPath(string folder, string fileName, string fileType)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", folder);
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            var name = $"{fileName}.{fileType}";
            var path = Path.Combine(folderPath, name);

            return path;
        }
    }
}