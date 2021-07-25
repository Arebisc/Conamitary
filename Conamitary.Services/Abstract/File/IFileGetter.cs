using Conamitary.Dtos.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Services.Abstract.File
{
    public interface IFileGetter
    {
        Task<IEnumerable<FileDto>> Get(Guid receipeId);
    }
}
