using System.Threading.Tasks;

namespace Conamitary.Database.Abstract.Receipe
{
    public interface IReceipeDeleter
    {
        Task<Models.Receipe> Delete(Models.Receipe receipe);
    }
}
