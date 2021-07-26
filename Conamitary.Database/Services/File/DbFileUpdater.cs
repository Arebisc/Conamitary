using Conamitary.Database.Abstract.File;
using System.Threading.Tasks;

namespace Conamitary.Database.Services.File
{
    public class DbFileUpdater : IDbFileUpdater
    {
        private readonly ConamitaryContext _context;

        public DbFileUpdater(ConamitaryContext context)
        {
            _context = context;
        }

        public Task<Models.File> Update(Models.File file)
        {
            _context.Files.Update(file);
            return Task.FromResult(file);
        }
    }
}
