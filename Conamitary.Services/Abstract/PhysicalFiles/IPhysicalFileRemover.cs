using System;
using System.Threading.Tasks;

namespace Conamitary.Services.Abstract.PhysicalFiles
{
    public interface IPhysicalFileRemover
    {
        Task<bool> Remove(Guid fileId, string extension);
    }
}
