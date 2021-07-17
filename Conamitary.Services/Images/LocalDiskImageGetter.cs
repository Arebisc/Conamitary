using Conamitary.Database;
using Conamitary.Services.Abstract.Files;
using Conamitary.Services.Abstract.Images;
using Conamitary.Services.Commons.ServiceResults;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Services.Images
{
    public class LocalDiskImageGetter: IReceipeImageGetter
    {
        private readonly string _directoryPath;
        private readonly IFileGetter _fileGetter;

        public LocalDiskImageGetter(
            IFileGetter fileGetter,
            IConfiguration configuration)
        {
            _directoryPath = configuration.GetSection("FilesLocalPath").Value;
            _fileGetter = fileGetter;
        }

        public async Task<FileGetterResult> Get(Guid fileId)
        {
            var imageEntity = await _fileGetter.Get(fileId);
            if(imageEntity == null)
            {
                return null;
            }

            var fullFilePath = Path.Combine(_directoryPath, fileId.ToString() + imageEntity.Extension);
            if (!File.Exists(fullFilePath))
            {
                return null;
            }

            var fs = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            return new FileGetterResult
            {
                Stream = fs,
                ContentType = imageEntity.ContentType
            };
        }
    }
}
