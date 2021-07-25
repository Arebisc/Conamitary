using Conamitary.Services.Commons.ServiceResults;
using System;

namespace Conamitary.Services.Abstract.PhysicalFiles
{
    public interface IPhysicalFileGetter
    {
        FileGetterResult Get(Guid fileId, string extension, string contentType);
    }
}
