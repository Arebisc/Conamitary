using AutoMapper;
using Conamitary.Database;
using Conamitary.Dtos.Receipes;
using Conamitary.Services.Abstract.Receipe;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

            _context.Receips.Add(model);
            await SaveImages(model, receipeDto.Images);

            return _mapper.Map<ReceipeDto>(model);
        }

        private async Task SaveImages(Database.Models.Receipe receipe, IEnumerable<IFormFile> formFiles)
        {
            var filesToBeSaved = new List<IFormFile>();

            foreach (var file in formFiles)
            {
                var fileModel = new Database.Models.File
                {
                    ReceipeId = receipe.Id,
                    Id = Guid.NewGuid()
                };

                _context.Images.Add(fileModel);
                filesToBeSaved.Add(file);
            }
            await _context.SaveChangesAsync();

            await SendFilesToFilesMicroservice(filesToBeSaved, receipe.Id);
        }

        private async Task SendFilesToFilesMicroservice(IEnumerable<IFormFile> files, Guid fileId)
        {
            var url = $"{_fileApiUrl}/api/file";
            using var httpClient = _httpClientFactory.CreateClient();

            foreach (var file in files)
            {
                using (var content = new MultipartFormDataContent())
                using (var fileStream = file.OpenReadStream())
                {
                    content.Add(new StreamContent(fileStream), "File", file.FileName);
                    content.Add(new StringContent(fileId.ToString()), "FileId");
                    
                    await httpClient.PostAsync(url, content);
                }
            }
        }
    }
}
