using Conamitary.Services.Abstract.PhysicalFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Conamitary.Services.PhysicalFiles
{
    public class LocalDiskFileSaver : IPhysicalFileSaver
    {
        private readonly string _savePath;
        private readonly ILogger<LocalDiskFileSaver> _logger;

        public LocalDiskFileSaver(
            IConfiguration configuration,
            ILogger<LocalDiskFileSaver> logger)
        {
            _savePath = configuration.GetSection("FilesLocalPath").Value;
            _logger = logger;
        }

        public async Task<bool> Save(Guid fileId, string extension, Stream fileStream)
        {
            var fullSavePath = GetSavePath(fileId, extension);
            
            _logger.LogInformation($"Saving physical file at location: {fullSavePath}.");
            var saveResult = await SaveFileToDisk(fullSavePath, fileStream);

            return saveResult;
        }

        private string GetSavePath(Guid fileId, string fileExtension)
        {
            return Path.Combine(_savePath, fileId.ToString() + fileExtension);
        }

        private async Task<bool> SaveFileToDisk(string fullSavePath, Stream sourceStream)
        {
            try
            {
                using var destinationStream = new FileStream(fullSavePath, FileMode.Create);
                sourceStream.Seek(0, SeekOrigin.Begin);
                await sourceStream.CopyToAsync(destinationStream);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while saving file!");
                return false;
            }
        }
    }
}
