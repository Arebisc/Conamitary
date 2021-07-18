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
        private readonly IHttpClientFactory _httpClientFactory;

        public ReceipeRemover(
            ConamitaryContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
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

            await RemoveImages(receipe.Images.Select(x => x.Id));

            return _mapper.Map<ReceipeDto>(receipe);
        }

        public async Task RemoveImages(IEnumerable<Guid> fileIds)
        {
            if(!fileIds.Any())
            {
                return;
            }

            using var client = _httpClientFactory.CreateClient(ApiTypes.FileApi);
            foreach (var fileId in fileIds)
            {
                var url = $"/api/file/{fileId}";
                await client.DeleteAsync(url);
            }
        }
    }
}
