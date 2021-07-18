using Conamitary.Services.Abstract.Files;
using Conamitary.Services.Abstract.Receipe;
using Conamitary.Services.Commons;
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
        private readonly IReceipeFileRemover _receipeFileRemover;

        public ImagesController(
            IHttpClientFactory httpClientFactory,
            IFileGetter fileGetter,
            IReceipeFileRemover receipeFileRemover)
        {
            _httpClientFactory = httpClientFactory;
            _fileGetter = fileGetter;
            _receipeFileRemover = receipeFileRemover;
        }

        [HttpGet("{imageId}")]
        public async Task<IActionResult> Get(Guid imageId)
        {
            if (Guid.Empty == imageId)
            {
                return BadRequest();
            }
            using var client = _httpClientFactory.CreateClient(ApiTypes.FileApi);
            var fileStream = await client.GetStreamAsync($"api/file/{imageId}");

            var contentType = await _fileGetter.GetContentType(imageId);

            return File(fileStream, contentType);
        }

        [HttpDelete("{imageId}/{receipeId}")]
        public async Task<IActionResult> Delete(Guid imageId, Guid receipeId)
        {
            if (Guid.Empty == imageId)
            {
                return BadRequest();
            }
            await _receipeFileRemover.RemoveFileFromReceipe(imageId, receipeId);
            return Ok();
        }
    }
}
