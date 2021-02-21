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
    public class ReceipeUpdater : IReceipeUpdater
    {
        private readonly ConamitaryContext _context;
        private readonly IMapper _mapper;

        public ReceipeUpdater(
            ConamitaryContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReceipeDto> Update(Guid receipeId, ReceipeDto receipeDto)
        {
            var receipe = await _context.Receips.FirstOrDefaultAsync(x => x.Id == receipeId);
            if (receipe == null)
            {
                throw new ArgumentException($"Receipe with Id: {receipeId} does not exist");
            }
             _mapper.Map(receipeDto, receipe);

            _context.Receips.Update(receipe);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReceipeDto>(receipe);
        }
    }
}
