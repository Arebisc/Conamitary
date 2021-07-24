using Conamitary.Database.Abstract.Receipe;
using System.Threading.Tasks;

namespace Conamitary.Database.Services.Receipe
{
    public class ReceipeDeleter: IReceipeDeleter
    {
        private readonly ConamitaryContext _conamitaryContext;

        public ReceipeDeleter(ConamitaryContext conamitaryContext)
        {
            _conamitaryContext = conamitaryContext;
        }

        public Task<Models.Receipe> Delete(Models.Receipe receipe)
        {
            _conamitaryContext.Receipes.Remove(receipe);
            return Task.FromResult(receipe);
        }
    }
}
