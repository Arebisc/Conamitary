using Conamitary.Services.Abstract.Files;
using Conamitary.Web.Configuration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Conamitary.Web.Controllers
{
    [Route("api/images")]
    public class ImagesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IFileGetter _fileGetter;

        public ImagesController(
            IHttpClientFactory httpClientFactory,
            IFileGetter fileGetter)
        {
            _httpClientFactory = httpClientFactory;
            _fileGetter = fileGetter;
        }

        [HttpGet("{imageId}")]
        public async Task<IActionResult> Get(Guid imageId)
        {
            if (Guid.Empty == imageId)
            {
                return BadRequest();
            }
            var client = _httpClientFactory.CreateClient(ApiTypes.FileApi);
            var fileStream = await client.GetStreamAsync($"api/file/{imageId}");

            var contentType = await _fileGetter.GetContentType(imageId);

            return File(fileStream, contentType);
        }
    }
}
