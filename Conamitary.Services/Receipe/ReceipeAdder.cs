using AutoMapper;
using Conamitary.Database.Abstract;
using Conamitary.Database.Abstract.Receipe;
using Conamitary.Dtos.Receipes;
using Conamitary.Services.Abstract.Receipe;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace Conamitary.Services.Receipe
{
    public class ReceipeAdder : IReceipeAdder
    {
        private readonly IDbReceipeAdder _dbReceipeAdder;
        private readonly IDbContextSaver _dbContextSaver;
        private readonly IMapper _mapper;
        private readonly ILogger<ReceipeAdder> _logger;

        public ReceipeAdder(
            IDbReceipeAdder dbReceipeAdder,
            IDbContextSaver dbContextSaver,
            IMapper mapper,
            ILogger<ReceipeAdder> logger)
        {
            _dbReceipeAdder = dbReceipeAdder;
            _dbContextSaver = dbContextSaver;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ReceipeDto> Add(AddReceipeDto receipeDto)
        {
            var model = _mapper.Map<Database.Models.Receipe>(receipeDto);

            await _dbReceipeAdder.Add(model);
            await _dbContextSaver.SaveChangesAsync();

            _logger.LogDebug("Added new receipe: ", JsonSerializer.Serialize(model));

            return _mapper.Map<ReceipeDto>(model);
        }
    }
}
