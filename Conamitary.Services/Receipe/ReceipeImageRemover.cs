using Conamitary.Database.Abstract;
using Conamitary.Database.Abstract.Receipe;
using Conamitary.Services.Abstract.PhysicalFiles;
using Conamitary.Services.Abstract.Receipe;
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

        public ReceipeImageRemover(
            IDbReceipeGetter dbReceipeGetter,
            IDbContextSaver dbContextSaver,
            IPhysicalFileRemover physicalFileRemover,
            IDbFileGetter dbFileGetter,
            IDbFileDeleter dbFileDeleter)
        {
            _dbReceipeGetter = dbReceipeGetter;
            _dbContextSaver = dbContextSaver;
            _physicalFileRemover = physicalFileRemover;
            _dbFileGetter = dbFileGetter;
            _dbFileDeleter = dbFileDeleter;
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

            receipeModel.Images.Remove(fileModel);
            await _dbContextSaver.SaveChangesAsync();

            if (fileModel.Receipes.Any())
            {
                return;
            }

            await _physicalFileRemover.Remove(fileId, fileModel.Extension);
        }

        public async Task RemoveImagesByIds(IEnumerable<Guid> filesIds)
        {
            var files = (await _dbFileGetter.Get(true))
                .Where(x => filesIds.Contains(x.Id));

            foreach(var file in files)
            {
                if (file.Receipes.Any())
                {
                    string receipesIds = string.Join(", ", file.Receipes.Select(x => x.Id));
                    throw new InvalidOperationException($"File with id: {file.Id} is still in use by receipes with ids: {receipesIds}");
                }

                await _dbFileDeleter.Delete(file);
                await _physicalFileRemover.Remove(file.Id, file.Extension);
            }
        }
    }
}
