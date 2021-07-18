using Conamitary.Database;
using Conamitary.Dtos.Files;
using Conamitary.Services.Abstract.Receipe;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Services.Receipe
{
    public class ReceipeFileAdder: IReceipeFileAdder
    {
        private readonly string _fileApiUrl;
        private readonly ConamitaryContext _conamitaryContext;
        private readonly IHttpClientFactory _httpClientFactory;

        public ReceipeFileAdder(
            ConamitaryContext conamitaryContext,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _conamitaryContext = conamitaryContext;
            _httpClientFactory = httpClientFactory;
            _fileApiUrl = configuration.GetSection("FileApiUrl").Value;
        }

        public async Task Add(Guid receipeId, IEnumerable<IFormFile> files)
        {
            var receipeEntity = _conamitaryContext.Receipes.FirstOrDefaultAsync(x => x.Id == receipeId);
            if(receipeEntity == null)
            {
                throw new ArgumentException($"Receipe with id: {receipeId} does not exist");
            }

            var url = $"{_fileApiUrl}/api/file";
            using var httpClient = _httpClientFactory.CreateClient();

            foreach (var file in files)
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
