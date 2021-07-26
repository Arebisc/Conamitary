using System.Threading.Tasks;

namespace Conamitary.Database.Abstract.Receipe
{
    public interface IDbReceipeDeleter
    {
        Task<Models.Receipe> Delete(Models.Receipe receipe);
    }
}
