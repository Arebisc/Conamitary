using Conamitary.Services.Abstract.PhysicalFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Conamitary.Services.PhysicalFiles
{
    public class LocalDiskPathCreator : IPhysicalPathCreator
    {
        private readonly string _directoryPath;
        private readonly ILogger<LocalDiskPathCreator> _logger;

        public LocalDiskPathCreator(
            IConfiguration configuration,
            ILogger<LocalDiskPathCreator> logger)
        {
            _directoryPath = configuration.GetSection("FilesLocalPath").Value;
            _logger = logger;
        }

        public Task<bool> CreateFilesSavePath()
        {
            try
            {
                Directory.CreateDirectory(_directoryPath);
                return Task.FromResult(true);
            }
            catch(UnauthorizedAccessException ex)
            {
                _logger.LogError($"Cannot access: {_directoryPath}!", ex);
                return Task.FromResult(false);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Cannot create directory at: {_directoryPath}!", ex);
                return Task.FromResult(false);
            }
        }
    }
}
