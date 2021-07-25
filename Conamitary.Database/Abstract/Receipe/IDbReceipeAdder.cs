using System.Threading.Tasks;

namespace Conamitary.Database.Abstract.Receipe
{
    public interface IDbReceipeAdder
    {
        Task<Models.Receipe> Add(Models.Receipe file);
    }
}
