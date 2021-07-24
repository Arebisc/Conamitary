using System.Threading.Tasks;

namespace Conamitary.Database.Abstract
{
    public interface IDbContextSaver
    {
        Task SaveChangesAsync();
    }
}
