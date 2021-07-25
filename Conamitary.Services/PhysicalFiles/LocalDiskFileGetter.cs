using Conamitary.Services.Abstract.PhysicalFiles;
using Conamitary.Services.Commons.ServiceResults;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Conamitary.Services.PhysicalFiles
{
    public class LocalDiskFileGetter: IPhysicalFileGetter
    {
        private readonly string _directoryPath;

        public LocalDiskFileGetter(
            IConfiguration configuration)
        {
            _directoryPath = configuration.GetSection("FilesLocalPath").Value;
        }

        public FileGetterResult Get(Guid fileId, string extension, string contentType)
        {
            var fullFilePath = Path.Combine(_directoryPath, fileId.ToString() + extension);
            if (!File.Exists(fullFilePath))
            {
                return null;
            }

            var fs = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return new FileGetterResult
            {
                Stream = fs,
                ContentType = contentType
            };
        }
    }
}
