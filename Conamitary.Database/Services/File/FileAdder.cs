using Conamitary.Database.Abstract.File;
using System.Threading.Tasks;

namespace Conamitary.Database.Services.File
{
    public class FileAdder : IFileAdder
    {
        private readonly ConamitaryContext _context;

        public FileAdder(ConamitaryContext context)
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
