using System.Threading.Tasks;

namespace Conamitary.Database.Abstract.File
{
    public interface IFileAdder
    {
        Task<Models.File> Add(Models.File file);
    }
}
