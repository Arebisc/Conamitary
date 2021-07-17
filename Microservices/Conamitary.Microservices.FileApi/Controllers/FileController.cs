using Conamitary.Dtos.Files;
using Conamitary.Services.Abstract.Images;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Conamitary.Microservices.FileApi.Controllers
{
    [Route("api/file")]
    public class FileController : ControllerBase
    {
        private readonly IReceipeImageSaver _receipeImageSaver;
        private readonly IReceipeImageGetter _receipeImageGetter;

        public FileController(
            IReceipeImageSaver receipeImageSaver,
            IReceipeImageGetter receipeImageGetter)
        {
            _receipeImageSaver = receipeImageSaver;
            _receipeImageGetter = receipeImageGetter;
        }

        [HttpPost]
        public async Task<IActionResult> SaveFiles([FromForm] SaveReceipeImageDto saveFileContentDto)
        {
            var result = await _receipeImageSaver.Save(saveFileContentDto);
            switch (result)
            {
                case Services.Commons.ServiceResults.FileSaverResultEnum.Ok:
                    return Ok();
                case Services.Commons.ServiceResults.FileSaverResultEnum.AlreadyExists:
                    return StatusCode(403);
                default:
                    return StatusCode(500);
            }
        }

        [HttpGet("{fileId}")]
        public async Task<IActionResult> GetFile(Guid fileId)
        {
            var result = await _receipeImageGetter.Get(fileId);
            if(result == null)
            {
                return StatusCode(404);
            }

            var imageStream = result.Stream;
            return File(imageStream, result.ContentType);
        }
    }
}
