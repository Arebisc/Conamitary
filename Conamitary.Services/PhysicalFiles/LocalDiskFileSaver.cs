using Conamitary.Services.Abstract.PhysicalFiles;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Conamitary.Services.PhysicalFiles
{
    public class LocalDiskFileSaver : IPhysicalFileSaver
    {
        private readonly string _savePath;

        public LocalDiskFileSaver(
            IConfiguration configuration)
        {
            _savePath = configuration.GetSection("FilesLocalPath").Value;
        }

        public async Task<bool> Save(Guid fileId, string extension, Stream fileStream)
        {
            var fullSavePath = GetSavePath(fileId, extension);
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
                return false;
            }
        }
    }
}
