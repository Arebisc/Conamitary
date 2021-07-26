using Conamitary.Database.Abstract.Receipe;
using System.Threading.Tasks;

namespace Conamitary.Database.Services.File
{
    public class DbFileDeleter : IDbFileDeleter
    {
        private readonly ConamitaryContext _conamitaryContext;

        public DbFileDeleter(ConamitaryContext conamitaryContext)
        {
            _conamitaryContext = conamitaryContext;
        }

        public Task<Models.File> Delete(Models.File file)
        {
            _conamitaryContext.Files.Remove(file);
            return Task.FromResult(file);
        }
    }
}
