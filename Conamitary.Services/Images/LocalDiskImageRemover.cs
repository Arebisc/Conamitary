using Conamitary.Database;
using Conamitary.Services.Abstract.Images;
using Conamitary.Services.Commons.ServiceResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Conamitary.Services.Images
{
    public class LocalDiskImageRemover : IReceipeImageRemover
    {
        private readonly string _filesLocalPath;
        private readonly ConamitaryContext _conamitaryContext;

        public LocalDiskImageRemover(
            ConamitaryContext conamitaryContext,
            IConfiguration configuration)
        {
            _filesLocalPath = configuration.GetSection("FilesLocalPath").Value;
            _conamitaryContext = conamitaryContext;
        }

        public async Task<FileRemoverResult> Remove(Guid fileId)
        {
            var fileEntity = await _conamitaryContext.Files
                .Include(x => x.Receipes)
                .FirstOrDefaultAsync( x=> x.Id == fileId);
            if (fileEntity == null)
            {
                return FileRemoverResult.FileNotFound;
            }

            var fullPath = GetFileFullPath(fileId, fileEntity.Extension);
            if (!File.Exists(fullPath))
            {
                return FileRemoverResult.Error;
            }

            if (fileEntity.Receipes.Any())
            {
                return FileRemoverResult.StillInUse;
            }

            await DeletePhysicalFileAsync(fullPath);

            _conamitaryContext.Files.Remove(fileEntity);
            await _conamitaryContext.SaveChangesAsync();

            return FileRemoverResult.Ok;
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
