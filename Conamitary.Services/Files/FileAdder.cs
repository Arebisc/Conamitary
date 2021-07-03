using Conamitary.Database;
using Conamitary.Database.Models;
using Conamitary.Services.Abstract.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Services.Files
{
    public class FileAdder : IFileAdder
    {
        private readonly ConamitaryContext _context;

        public FileAdder(ConamitaryContext context)
        {
            _context = context;
        }

        public Task<File> Add(File file)
        {
            _context.Add(file);
            return Task.FromResult(file);
        }
    }
}
