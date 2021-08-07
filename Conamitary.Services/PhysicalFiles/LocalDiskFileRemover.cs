using Conamitary.Services.Abstract.PhysicalFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Conamitary.Services.PhysicalFiles
{
    public class LocalDiskFileRemover : IPhysicalFileRemover
    {
        private readonly string _filesLocalPath;
        private readonly ILogger<LocalDiskFileRemover> _logger;

        public LocalDiskFileRemover(
            IConfiguration configuration,
            ILogger<LocalDiskFileRemover> logger)
        {
            _filesLocalPath = configuration.GetSection("FilesLocalPath").Value;
            _logger = logger;
        }

        public async Task<bool> Remove(Guid fileId, string extension)
        {
            var fullPath = GetFileFullPath(fileId, extension);
            if (!File.Exists(fullPath))
            {
                _logger.LogWarning($"File {fullPath} does not exist.");
                return false;
            }

            _logger.LogDebug($"Removing physical file at location: {fullPath}");

            await DeletePhysicalFileAsync(fullPath);
            return true;
        }

        private string GetFileFullPath(Guid fileId, string fileExtension)
        {
            return Path.Combine(_filesLocalPath, fileId.ToString() + fileExtension);
        }

        private Task DeletePhysicalFileAsync(string filePath)
        {
            return Task.Run(() =>
            {
                File.Delete(filePath);
            });
        }
    }
}
