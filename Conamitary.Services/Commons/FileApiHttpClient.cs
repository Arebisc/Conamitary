using Conamitary.Services.Abstract.Commons;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<FileApiHttpClient> _logger;
        private readonly string _fileApiUrl;

        public FileApiHttpClient(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ILogger<FileApiHttpClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
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

            try
            {
                _logger.LogInformation($"Sending files with ids: {string.Join(', ', filesIds)}" +
                    $" to file microservice.");
                await httpClient.SendAsync(request);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error while requesting files to remove.");
                _logger.LogError(ex.Message, ex.StackTrace);
            }
        }
    }
}
