using AutoMapper;
using Conamitary.Database;
using Conamitary.Dtos;
using Conamitary.Dtos.Receipes;
using Conamitary.Services.Abstract.Receipe;
using Conamitary.Services.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Services.Receipe
{
    public class ReceipeRemover : IReceipeRemover
    {
        private readonly ConamitaryContext _context;
        private readonly IMapper _mapper;
        private readonly IReceipeFileRemover _receipeFileRemover;

        public ReceipeRemover(
            ConamitaryContext context,
            IMapper mapper,
            IReceipeFileRemover receipeFileRemover)
        {
            _context = context;
            _mapper = mapper;
            _receipeFileRemover = receipeFileRemover;
        }

        public async Task<ReceipeDto> Remove(Guid receipeId)
        {
            var receipe = await _context.Receipes
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == receipeId);
            if (receipe == null)
            {
                throw new ArgumentException($"Receipe with Id: {receipeId} does not exist");
            }
            _context.Receipes.Remove(receipe);
            await _context.SaveChangesAsync();

            await _receipeFileRemover.RemoveFilesByIds(receipe.Images.Select(x => x.Id));

            return _mapper.Map<ReceipeDto>(receipe);
        }
    }
}
