using Conamitary.Database.Abstract.Receipe;
using System;
using System.Threading.Tasks;

namespace Conamitary.Database.Services.Receipe
{
    public class ReceipeUpdater : IReceipeUpdater
    {
        private readonly ConamitaryContext _context;

        public ReceipeUpdater(ConamitaryContext context)
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
