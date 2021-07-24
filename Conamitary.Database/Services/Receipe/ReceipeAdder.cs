using Conamitary.Database.Abstract.Receipe;
using System.Threading.Tasks;

namespace Conamitary.Database.Services.Receipe
{
    public class ReceipeAdder : IReceipeAdder
    {
        private readonly ConamitaryContext _context;

        public ReceipeAdder(ConamitaryContext context)
        {
            _context = context;
        }

        public Task<Models.Receipe> Add(Models.Receipe receipe)
        {
            _context.Receipes.Add(receipe);
            return Task.FromResult(receipe);
        }
    }
}
