using System.Threading.Tasks;

namespace Conamitary.Database.Abstract.Receipe
{
    public interface IReceipeAdder
    {
        Task<Models.Receipe> Add(Models.Receipe file);
    }
}
