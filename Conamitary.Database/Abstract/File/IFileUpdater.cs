using System.Threading.Tasks;

namespace Conamitary.Database.Abstract.File
{
    public interface IFileUpdater
    {
        Task<Models.File> Update(Models.File file);
    }
}
