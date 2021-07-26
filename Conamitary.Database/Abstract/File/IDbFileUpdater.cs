using System.Threading.Tasks;

namespace Conamitary.Database.Abstract.File
{
    public interface IDbFileUpdater
    {
        Task<Models.File> Update(Models.File file);
    }
}
