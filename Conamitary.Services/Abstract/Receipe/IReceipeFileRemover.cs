using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Services.Abstract.Receipe
{
    public interface IReceipeFileRemover
    {
        public Task RemoveFileFromReceipe(Guid fileId, Guid receipeId);
        public Task RemoveFilesByIds(IEnumerable<Guid> fileIds);
    }
}
