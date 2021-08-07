using Conamitary.Services.Abstract.PhysicalFiles;
using Conamitary.Services.Commons.ServiceResults;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Conamitary.Services.PhysicalFiles
{
    public class LocalDiskFileGetter: IPhysicalFileGetter
    {
        private readonly string _directoryPath;
        private readonly ILogger<LocalDiskFileGetter> _logger;

        public LocalDiskFileGetter(
            IConfiguration configuration,
            ILogger<LocalDiskFileGetter> logger)
        {
            _directoryPath = configuration.GetSection("FilesLocalPath").Value;
            _logger = logger;
        }

        public FileGetterResult Get(Guid fileId, string extension, string contentType)
        {
            var fullFilePath = Path.Combine(_directoryPath, fileId.ToString() + extension);
            if (!File.Exists(fullFilePath))
            {
                _logger.LogWarning($"File at location: {fullFilePath} does not exist");
                return null;
            }
            
            var fs = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            _logger.LogInformation($"Opened read stream to file: {fullFilePath}");
            return new FileGetterResult
            {
                Stream = fs,
                ContentType = contentType
            };
        }
    }
}
