using Conamitary.Database.Abstract;
using Conamitary.Database.Abstract.File;
using Conamitary.Database.Abstract.Receipe;
using Conamitary.Database.Models;
using Conamitary.Services.Abstract.Commons;
using Conamitary.Services.Abstract.PhysicalFiles;
using Conamitary.Services.Abstract.Receipe;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Conamitary.Services.Receipe
{
    public class ReceipeImageAdder: IReceipeImageAdder
    {
        private readonly IDbReceipeGetter _dbReceipeGetter;
        private readonly IDbFileAdder _dbFileAdder;
        private readonly IDbFileGetter _dbFileGetter;
        private readonly IDbContextSaver _dbContextSaver;
        private readonly IPhysicalFileSaver _physicalFileSaver;
        private readonly IMd5Calculator _md5Calculator;

        public ReceipeImageAdder(
            IDbReceipeGetter dbReceipeGetter,
            IDbFileAdder dbFileAdder,
            IDbFileGetter dbFileGetter,
            IDbContextSaver dbContextSaver,
            IPhysicalFileSaver physicalFileSaver,
            IMd5Calculator md5Calculator)
        {
            _dbReceipeGetter = dbReceipeGetter;
            _dbFileAdder = dbFileAdder;
            _dbFileGetter = dbFileGetter;
            _dbContextSaver = dbContextSaver;
            _physicalFileSaver = physicalFileSaver;
            _md5Calculator = md5Calculator;
        }

        public async Task<Guid> Add(Guid receipeId, IFormFile file)
        {
            var receipeEntity = await _dbReceipeGetter.Get(receipeId, true);
            if(receipeEntity == null)
            {
                throw new ArgumentException($"Receipe with id: {receipeId} does not exist");
            }

            using var fileStream = file.OpenReadStream();
            var md5Checksum = _md5Calculator.CalculateHash(fileStream);
            var existingFileModel = await _dbFileGetter.Get(md5Checksum);

            if (existingFileModel != null)
            {
                receipeEntity.Images.Add(existingFileModel);
                await _dbContextSaver.SaveChangesAsync();
                return existingFileModel.Id;
            }
            else
            {
                var fileModel = new File
                {
                    Id = Guid.NewGuid(),
                    Md5Checksum = md5Checksum,
                    ContentType = file.ContentType,
                    Extension = System.IO.Path.GetExtension(file.FileName),
                };

                await _dbFileAdder.Add(fileModel);
                receipeEntity.Images.Add(fileModel);
                await _dbContextSaver.SaveChangesAsync();

                await _physicalFileSaver.Save(fileModel.Id, fileModel.Extension, fileStream);

                return fileModel.Id;
            }
        }
    }
}
