using Conamitary.Database.Abstract.Receipe;
using System;
using System.Threading.Tasks;

namespace Conamitary.Database.Services.Receipe
{
    public class DbReceipeUpdater : IDbReceipeUpdater
    {
        private readonly ConamitaryContext _context;

        public DbReceipeUpdater(ConamitaryContext context)
        {
            _context = context;
        }

        public Task<Models.Receipe> Update(Guid id, Models.Receipe receipe)
        {
            _context.Receipes.Update(receipe);
            return Task.FromResult(receipe);
        }
    }
}
