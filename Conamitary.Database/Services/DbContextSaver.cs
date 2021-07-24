using Conamitary.Database.Abstract;
using System.Threading.Tasks;

namespace Conamitary.Database.Services
{
    public class DbContextSaver : IDbContextSaver
    {
        private readonly ConamitaryContext _context;

        public DbContextSaver(ConamitaryContext context)
        {
            _context = context;
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
