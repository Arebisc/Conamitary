using Conamitary.Database;
using Conamitary.Database.Models;
using Conamitary.Services.Abstract.Files;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Conamitary.Services.Files
{
    public class FileGetter : IFileGetter
    {
        private readonly ConamitaryContext _context;

        public FileGetter(ConamitaryContext context)
        {
            _context = context;
        }

        public Task<File> Get(string md5Checksum)
        {
            return _context.Files.FirstOrDefaultAsync(x => x.Md5Checksum == md5Checksum);
        }
    }
}
