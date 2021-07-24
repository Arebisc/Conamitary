using Conamitary.Database.Abstract.Receipe;
using System.Threading.Tasks;

namespace Conamitary.Database.Services.File
{
    public class FileDeleter : IFileDeleter
    {
        private readonly ConamitaryContext _conamitaryContext;

        public FileDeleter(ConamitaryContext conamitaryContext)
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
