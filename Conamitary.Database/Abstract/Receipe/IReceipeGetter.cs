using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conamitary.Database.Abstract.Receipe
{
    public interface IReceipeGetter
    {
        Task<Models.Receipe> Get(Guid recipeId, bool includeImages = false);
        Task<IEnumerable<Models.Receipe>> Get(bool includeImages = false);
    }
}
