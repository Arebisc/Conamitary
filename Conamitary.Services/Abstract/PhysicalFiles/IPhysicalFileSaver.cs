using System;
using System.IO;
using System.Threading.Tasks;

namespace Conamitary.Services.Abstract.PhysicalFiles
{
    public interface IPhysicalFileSaver
    {
        Task<bool> Save(Guid fileId, string extension, Stream fileStream);
    }
}
