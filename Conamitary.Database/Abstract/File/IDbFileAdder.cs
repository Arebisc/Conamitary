using System.Threading.Tasks;

namespace Conamitary.Database.Abstract.File
{
    public interface IDbFileAdder
    {
        Task<Models.File> Add(Models.File file);
    }
}
