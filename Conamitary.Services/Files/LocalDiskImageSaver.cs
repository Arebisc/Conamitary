﻿using Conamitary.Database;
using Conamitary.Services.Abstract.Commons;
using Conamitary.Services.Abstract.Files;
using Conamitary.Services.Commons.ServiceResults;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Conamitary.Services.Files
{
    public class LocalDiskImageSaver : IReceipeImageSaver
    {
        private readonly IMd5Calculator _md5Calculator;
        private readonly ConamitaryContext _conamitaryContext;
        private readonly string _savePath;

        public LocalDiskImageSaver(
            IMd5Calculator md5Calculator,
            IConfiguration configuration,
            ConamitaryContext conamitaryContext)
        {
            _md5Calculator = md5Calculator;
            _conamitaryContext = conamitaryContext;
            _savePath = configuration.GetSection("FilesLocalPath").Value;
        }

        public async Task<FileSaverResultEnum> Save(Guid receipeId, IFormFile formFile)
        {
            var receipe = await _conamitaryContext.Receipes
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == receipeId);
            if (receipe == null)
            {
                return FileSaverResultEnum.ReceipeDoesNotExist;
            }

            using (var sourceStream = formFile.OpenReadStream())
            {
                var md5Checksum = _md5Calculator.CalculateHash(sourceStream);

                var fileModel = await _conamitaryContext.Files.FirstOrDefaultAsync(x => x.Md5Checksum == md5Checksum);
                if (fileModel != null)
                {
                    receipe.Images.Add(fileModel);
                    await _conamitaryContext.SaveChangesAsync();

                    return FileSaverResultEnum.AlreadyExists;
                }
                else
                {
                    var fileExtension = Path.GetExtension(formFile.FileName);
                    var fullSavePath = GetSavePath(receipeId, fileExtension);
                    var saveResult = await SaveFileToDisk(fullSavePath, sourceStream);

                    if (!saveResult)
                    {
                        return FileSaverResultEnum.Error;
                    }

                    var fileToInsert = new Database.Models.File
                    {
                        Id = Guid.NewGuid(),
                        Md5Checksum = md5Checksum
                    };

                    _conamitaryContext.Files.Add(fileToInsert);
                    receipe.Images.Add(fileToInsert);
                    await _conamitaryContext.SaveChangesAsync();
                }
                 
                return FileSaverResultEnum.Ok; 
            }
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
