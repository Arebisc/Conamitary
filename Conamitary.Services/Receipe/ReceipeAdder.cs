using AutoMapper;
using Conamitary.Database.Abstract;
using Conamitary.Database.Abstract.Receipe;
using Conamitary.Dtos.Receipes;
using Conamitary.Services.Abstract.Receipe;
using System.Threading.Tasks;

namespace Conamitary.Services.Receipe
{
    public class ReceipeAdder : IReceipeAdder
    {
        private readonly IDbReceipeAdder _dbReceipeAdder;
        private readonly IDbContextSaver _dbContextSaver;
        private readonly IMapper _mapper;

        public ReceipeAdder(
            IDbReceipeAdder dbReceipeAdder,
            IDbContextSaver dbContextSaver,
            IMapper mapper)
        {
            _dbReceipeAdder = dbReceipeAdder;
            _dbContextSaver = dbContextSaver;
            _mapper = mapper;
        }

        public async Task<ReceipeDto> Add(AddReceipeDto receipeDto)
        {
            var model = _mapper.Map<Database.Models.Receipe>(receipeDto);

            await _dbReceipeAdder.Add(model);
            await _dbContextSaver.SaveChangesAsync();

            return _mapper.Map<ReceipeDto>(model);
        }
    }
}
