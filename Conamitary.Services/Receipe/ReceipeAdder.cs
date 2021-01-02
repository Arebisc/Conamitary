using AutoMapper;
using Conamitary.Database;
using Conamitary.Dtos;
using Conamitary.Services.Abstract.Receipe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Services.Receipe
{
    public class ReceipeAdder : IReceipeAdder
    {
        private readonly ConamitaryContext _context;
        private readonly IMapper _mapper;

        public ReceipeAdder(
            ConamitaryContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<ReceipeDto> Add(ReceipeDto receipeDto)
        {
            var model = _mapper.Map<Database.Models.Receipe>(receipeDto);
            _context.Receips.Add(model);
            return Task.FromResult(_mapper.Map<ReceipeDto>(model));
        }
    }
}
