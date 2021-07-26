﻿using Conamitary.Services.Abstract.Commons;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Conamitary.Services.Commons
{
    public class FileApiHttpClient : IFileApiHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _fileApiUrl;

        public FileApiHttpClient(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _fileApiUrl = configuration.GetSection("FileApiUrl").Value;
        }

        public async Task InvokeDeleteFiles(IEnumerable<Guid> filesIds)
        {
            var url = $"{_fileApiUrl}/api/images";
            using var httpClient = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(filesIds),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url)
            };
            await httpClient.SendAsync(request);
        }
    }
}
