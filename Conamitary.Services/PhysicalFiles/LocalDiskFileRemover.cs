using Conamitary.Services.Abstract.PhysicalFiles;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Conamitary.Services.PhysicalFiles
{
    public class LocalDiskFileRemover : IPhysicalFileRemover
    {
        private readonly string _filesLocalPath;

        public LocalDiskFileRemover(
            IConfiguration configuration)
        {
            _filesLocalPath = configuration.GetSection("FilesLocalPath").Value;
        }

        public async Task<bool> Remove(Guid fileId, string extension)
        {
            var fullPath = GetFileFullPath(fileId, extension);
            if (!File.Exists(fullPath))
            {
                return false;
            }

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
