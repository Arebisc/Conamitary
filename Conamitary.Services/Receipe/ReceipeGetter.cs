using AutoMapper;
using Conamitary.Database.Abstract.Receipe;
using Conamitary.Dtos.Receipes;
using Conamitary.Services.Abstract.Receipe;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conamitary.Services.Receipe
{
    public class ReceipeGetter : IReceipeGetter
    {
        private readonly IDbReceipeGetter _dbReceipeGetter;
        private readonly IMapper _mapper;

        public ReceipeGetter(
            IDbReceipeGetter dbReceipeGetter,
            IMapper mapper)
        {
            _dbReceipeGetter = dbReceipeGetter;
            _mapper = mapper;
        }

        public async Task<ReceipeDto> Get(Guid recipeId)
        {
            var receipe = await _dbReceipeGetter.Get(recipeId);
            return _mapper.Map<ReceipeDto>(receipe);
        }

        public async Task<IEnumerable<ReceipeDto>> Get()
        {
            var receipes = await _dbReceipeGetter.Get(true);
            return _mapper.Map<IEnumerable<ReceipeDto>>(receipes);
        }
    }
}
