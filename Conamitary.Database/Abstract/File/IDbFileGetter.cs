using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conamitary.Database.Abstract.Receipe
{
    public interface IDbFileGetter
    {
        Task<Models.File> Get(Guid fileId, bool includeReceipes = false);
        Task<IEnumerable<Models.File>> Get(bool includeReceipes = false);
    }
}
