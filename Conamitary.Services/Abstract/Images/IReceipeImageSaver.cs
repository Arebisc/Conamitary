using Conamitary.Services.Commons.ServiceResults;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conamitary.Services.Abstract.Images
{
    public interface IReceipeImageSaver
    {
        Task<FileSaverResultEnum> Save(Guid receipeId, IFormFile file);
    }
}
