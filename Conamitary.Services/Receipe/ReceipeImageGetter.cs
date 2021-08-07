using Conamitary.Database.Abstract.Receipe;
using Conamitary.Services.Abstract.PhysicalFiles;
using Conamitary.Services.Abstract.Receipe;
using Conamitary.Services.Commons.ServiceResults;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Conamitary.Services.Receipe
{
    public class ReceipeImageGetter : IReceipeImageGetter
    {
        private readonly IDbFileGetter _dbFileGetter;
        private readonly IPhysicalFileGetter _physicalFileGetter;
        private readonly ILogger<ReceipeImageGetter> _logger;

        public ReceipeImageGetter(
            IDbFileGetter dbFileGetter,
            IPhysicalFileGetter physicalFileGetter,
            ILogger<ReceipeImageGetter> logger)
        {
            _dbFileGetter = dbFileGetter;
            _physicalFileGetter = physicalFileGetter;
            _logger = logger;
        }

        public async Task<FileGetterResult> Get(Guid fileId)
        {
            var imageModel = await _dbFileGetter.Get(fileId);
            if (imageModel == null)
            {
                _logger.LogWarning($"File with id: {fileId} not found");
                return null;
            }

            return _physicalFileGetter.Get(fileId,
                imageModel.Extension,
                imageModel.ContentType);
        }
    }
}
