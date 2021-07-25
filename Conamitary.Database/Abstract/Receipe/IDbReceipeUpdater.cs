using System;
using System.Threading.Tasks;

namespace Conamitary.Database.Abstract.Receipe
{
    public interface IDbReceipeUpdater
    {
        Task<Models.Receipe> Update(Guid id, Models.Receipe receipe);
    }
}
