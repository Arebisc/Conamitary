using AutoMapper;
using Conamitary.Database;
using Conamitary.Dtos;
using Conamitary.Services.Abstract.Receipe;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Services.Receipe
{
    public class ReceipeRemover : IReceipeRemover
    {
        private readonly ConamitaryContext _context;
        private readonly IMapper _mapper;

        public ReceipeRemover(
            ConamitaryContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReceipeDto> Remove(Guid receipeId)
        {
            var receipe = await _context.Receips.FirstOrDefaultAsync(x => x.Id == receipeId);
            if (receipe == null)
            {
                throw new ArgumentException($"Receipe with Id: {receipeId} does not exist");
            }
            _context.Receips.Remove(receipe);
            return _mapper.Map<ReceipeDto>(receipe);
        }
    }
}
