using Conamitary.Database.Abstract.Receipe;
using Conamitary.Services.Abstract.PhysicalFiles;
using Conamitary.Services.Abstract.Receipe;
using Conamitary.Services.Commons.ServiceResults;
using System;
using System.Threading.Tasks;

namespace Conamitary.Services.Receipe
{
    public class ReceipeImageGetter : IReceipeImageGetter
    {
        private readonly IDbFileGetter _dbFileGetter;
        private readonly IPhysicalFileGetter _physicalFileGetter;

        public ReceipeImageGetter(
            IDbFileGetter dbFileGetter,
            IPhysicalFileGetter physicalFileGetter)
        {
            _dbFileGetter = dbFileGetter;
            _physicalFileGetter = physicalFileGetter;
        }

        public async Task<FileGetterResult> Get(Guid fileId)
        {
            var imageModel = await _dbFileGetter.Get(fileId);
            if (imageModel == null)
            {
                return null;
            }

            return _physicalFileGetter.Get(fileId,
                imageModel.Extension,
                imageModel.ContentType);
        }
    }
}
