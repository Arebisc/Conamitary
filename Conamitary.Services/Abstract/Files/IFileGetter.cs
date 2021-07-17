using Conamitary.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Services.Abstract.Files
{
    public interface IFileGetter
    {
        Task<File> Get(string md5Checksum);
        Task<File> Get(Guid id);
        Task<string> GetContentType(Guid id);
    }
}
