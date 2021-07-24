using System.Threading.Tasks;

namespace Conamitary.Database.Abstract.Receipe
{
    public interface IFileDeleter
    {
        Task<Models.File> Delete(Models.File file);
    }
}
