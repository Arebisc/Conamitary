using Conamitary.Database.Abstract.File;
using System.Threading.Tasks;

namespace Conamitary.Database.Services.File
{
    public class DbFileAdder : IDbFileAdder
    {
        private readonly ConamitaryContext _context;

        public DbFileAdder(ConamitaryContext context)
        {
            _context = context;
        }

        public Task<Models.File> Add(Models.File file)
        {
            _context.Files.Add(file);
            return Task.FromResult(file);
        }
    }
}
