using AutoMapper;
using Conamitary.Database;
using Conamitary.Dtos;
using Conamitary.Dtos.Receipes;
using Conamitary.Services.Abstract.Receipe;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Services.Receipe
{
    public class ReceipeGetter : IReceipeGetter
    {
        private readonly ConamitaryContext _context;
        private readonly IMapper _mapper;

        public ReceipeGetter(
            ConamitaryContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReceipeDto> Get(Guid recipeId)
        {
            var receipe = await _context.Receips.FirstOrDefaultAsync(x => x.Id == recipeId);
            return _mapper.Map<ReceipeDto>(receipe);
        }

        public async Task<IEnumerable<ReceipeDto>> Get()
        {
            var receipes = await _context.Receips.ToArrayAsync();
            return _mapper.Map<IEnumerable<ReceipeDto>>(receipes);
        }
    }
}
