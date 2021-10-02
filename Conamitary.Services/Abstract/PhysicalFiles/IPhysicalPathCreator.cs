using System.Threading.Tasks;

namespace Conamitary.Services.Abstract.PhysicalFiles
{
    public interface IPhysicalPathCreator
    {
        Task<bool> CreateFilesSavePath();
    }
}
