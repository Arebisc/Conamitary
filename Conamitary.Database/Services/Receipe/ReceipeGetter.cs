using Conamitary.Database.Abstract.Receipe;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conamitary.Database.Services.Receipe
{
    public class ReceipeGetter: IReceipeGetter
    {
        private readonly ConamitaryContext _context;

        public ReceipeGetter(ConamitaryContext context)
        {
            _context = context;
        }

        public Task<Models.Receipe> Get(Guid receipeId, bool includeImages)
        {
            if (includeImages)
            {
                return _context.Receipes.
                    Include(x => x.Images)
                    .FirstOrDefaultAsync(x => x.Id == receipeId);
            }
            return _context.Receipes.FirstOrDefaultAsync(x => x.Id == receipeId);
        }

        public Task<IEnumerable<Models.Receipe>> Get(bool includeImages)
        {
            if (includeImages)
            {
                return Task.FromResult(_context.Receipes
                    .Include(x => x.Images)
                    .AsEnumerable());
            }

            return Task.FromResult(_context.Receipes.AsEnumerable());
        }
    }
}
