using Conamitary.Database.Abstract;
using Conamitary.Database.Abstract.Receipe;
using Conamitary.Services.Abstract.PhysicalFiles;
using Conamitary.Services.Abstract.Receipe;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conamitary.Services.Receipe
{
    public class ReceipeImageRemover: IReceipeImageRemover
    {
        private readonly IDbReceipeGetter _dbReceipeGetter;
        private readonly IDbContextSaver _dbContextSaver;
        private readonly IPhysicalFileRemover _physicalFileRemover;
        private readonly IDbFileGetter _dbFileGetter;
        private readonly IDbFileDeleter _dbFileDeleter;
        private readonly ILogger<ReceipeImageRemover> _logger;

        public ReceipeImageRemover(
            IDbReceipeGetter dbReceipeGetter,
            IDbContextSaver dbContextSaver,
            IPhysicalFileRemover physicalFileRemover,
            IDbFileGetter dbFileGetter,
            IDbFileDeleter dbFileDeleter,
            ILogger<ReceipeImageRemover> logger)
        {
            _dbReceipeGetter = dbReceipeGetter;
            _dbContextSaver = dbContextSaver;
            _physicalFileRemover = physicalFileRemover;
            _dbFileGetter = dbFileGetter;
            _dbFileDeleter = dbFileDeleter;
            _logger = logger;
        }

        public async Task RemoveImageFromReceipe(Guid fileId, Guid receipeId)
        {
            var receipeModel = await _dbReceipeGetter.Get(receipeId, true);
            if(receipeModel == null)
            {
                throw new ArgumentException($"Receipe with id: {receipeId} does not exist.");
            }

            var fileModel = receipeModel.Images.FirstOrDefault(x => x.Id == fileId);
            if(fileModel == null)
            {
                throw new InvalidOperationException($"Receipe with id: {receipeId} does not contain image" +
                    $"with id: {fileId}");
            }

            _logger.LogDebug($"Removing file with id: {fileModel.Id}");

            receipeModel.Images.Remove(fileModel);
            await _dbContextSaver.SaveChangesAsync();

            var fileStillInUse = await _dbFileGetter.IsFileStillInUse(fileModel.Id);
            if (fileStillInUse)
            {
                _logger.LogInformation($"File with id: {fileModel.Id} still in use");
                return;
            }

            await _physicalFileRemover.Remove(fileId, fileModel.Extension);
        }

        public async Task RemoveImagesByIds(IEnumerable<Guid> filesIds)
        {
            var files = (await _dbFileGetter.Get(true))
                .Where(x => filesIds.Contains(x.Id));

            _logger.LogDebug($"Removing files with ids: {string.Join(", ", files.Select(x => x.Id))}");

            foreach(var file in files)
            {
                if (!file.Receipes.Any())
                {
                    await _dbFileDeleter.Delete(file);
                    await _physicalFileRemover.Remove(file.Id, file.Extension);
                }
            }
        }
    }
}
