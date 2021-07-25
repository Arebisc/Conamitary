using Conamitary.Dtos.Files;
using Conamitary.Services.Abstract.PhysicalFiles;
using Conamitary.Services.Abstract.Receipe;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conamitary.Microservices.FileApi.Controllers
{
    [Route("api/images")]
    public class ImagesController : ControllerBase
    {
        private readonly IReceipeImageAdder _receipeImageAdder;
        private readonly IReceipeImageGetter _receipeImageGetter;
        private readonly IReceipeImageRemover _receipeImageRemover;

        public ImagesController(
            IReceipeImageAdder receipeImageAdder,
            IReceipeImageGetter receipeImageGetter,
            IReceipeImageRemover receipeImageRemover)
        {
            _receipeImageAdder = receipeImageAdder;
            _receipeImageGetter = receipeImageGetter;
            _receipeImageRemover = receipeImageRemover;
        }

        [HttpPost]
        public async Task<IActionResult> SaveImages([FromForm] SaveReceipeImageDto saveFileContentDto)
        {
            await _receipeImageAdder.Add(
                saveFileContentDto.ReceipeId,
                saveFileContentDto.File);
            return Ok();
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

        [HttpDelete("{receipeId}/{fileId}")]
        public async Task<IActionResult> RemoveFileFromReceipe(Guid receipeId, Guid fileId)
        {
            await _receipeImageRemover.RemoveImageFromReceipe(fileId, receipeId);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFile([FromBody] IEnumerable<Guid> filesIds)
        {
            await _receipeImageRemover.RemoveImagesByIds(filesIds);
            return Ok();
        }
    }
}
