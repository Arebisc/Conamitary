using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conamitary.Services.Abstract.Receipe
{
    public interface IReceipeImageRemover
    {
        public Task RemoveImageFromReceipe(Guid fileId, Guid receipeId);
        public Task RemoveImagesByIds(IEnumerable<Guid> fileIds);
    }
}
