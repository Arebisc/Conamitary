using Conamitary.Dtos.Files;
using Conamitary.Services.Abstract.Images;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conamitary.Microservices.FileApi.Controllers
{
    [Route("api/file")]
    public class FileController : ControllerBase
    {
        private readonly IReceipeImageSaver _receipeImageSaver;
        private readonly IReceipeImageGetter _receipeImageGetter;
        private readonly IReceipeImageRemover _receipeImageRemover;

        public FileController(
            IReceipeImageSaver receipeImageSaver,
            IReceipeImageGetter receipeImageGetter,
            IReceipeImageRemover receipeImageRemover)
        {
            _receipeImageSaver = receipeImageSaver;
            _receipeImageGetter = receipeImageGetter;
            _receipeImageRemover = receipeImageRemover;
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

        [HttpDelete("{fileId}")]
        public async Task<IActionResult> RemoveFile(Guid fileId)
        {
            var result = await _receipeImageRemover.Remove(fileId);
            if(result == Services.Commons.ServiceResults.FileRemoverResult.FileNotFound)
            {
                return StatusCode(404);
            }
            else if(result == Services.Commons.ServiceResults.FileRemoverResult.Error)
            {
                return StatusCode(400);
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFile([FromBody] IEnumerable<Guid> filesIds)
        {
            var result = await _receipeImageRemover.Remove(filesIds);
            return Ok(result);
        }
    }
}
