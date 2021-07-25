using System.Threading.Tasks;

namespace Conamitary.Database.Abstract.Receipe
{
    public interface IDbFileDeleter
    {
        Task<Models.File> Delete(Models.File file);
    }
}
