using AutoMapper;
using Conamitary.Database;
using Conamitary.Dtos.Files;
using Conamitary.Dtos.Receipes;
using Conamitary.Services.Abstract.Commons;
using Conamitary.Services.Abstract.Receipe;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Conamitary.Services.Receipe
{
    public class ReceipeAdder : IReceipeAdder
    {
        private readonly ConamitaryContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _fileApiUrl;

        public ReceipeAdder(
            ConamitaryContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _fileApiUrl = configuration.GetSection("FileApiUrl").Value;
        }

        public async Task<ReceipeDto> Add(AddReceipeDto receipeDto)
        {
            var model = _mapper.Map<Database.Models.Receipe>(receipeDto);

            _context.Receipes.Add(model);

            await _context.SaveChangesAsync();
            await SaveImages(model.Id, receipeDto.Images);

            return _mapper.Map<ReceipeDto>(model);
        }

        private async Task SaveImages(Guid receipeId, IEnumerable<IFormFile> formFiles)
        {
            var url = $"{_fileApiUrl}/api/file";
            using var httpClient = _httpClientFactory.CreateClient();

            foreach (var file in formFiles)
            {
                using (var content = new MultipartFormDataContent())
                using (var fileStream = file.OpenReadStream())
                {
                    content.Add(new StreamContent(fileStream), "File", file.FileName);
                    content.Add(new StringContent(receipeId.ToString()), "receipeId");
                    content.Add(new StringContent(Path.GetExtension(file.FileName)), nameof(SaveReceipeImageDto.Extension));
                    content.Add(new StringContent(file.ContentType), nameof(SaveReceipeImageDto.ContentType));

                    await httpClient.PostAsync(url, content);
                }
            }
        }
    }
}
