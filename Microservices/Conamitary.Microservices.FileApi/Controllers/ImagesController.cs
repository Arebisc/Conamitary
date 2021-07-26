using Conamitary.Dtos.Files;
using Conamitary.Services.Abstract.Receipe;
using Microsoft.AspNetCore.Cors;
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
        public async Task<IActionResult> SaveImage([FromForm] SaveReceipeImageDto saveFileContentDto)
        {
            var addedFileId = await _receipeImageAdder.Add(
                saveFileContentDto.ReceipeId,
                saveFileContentDto.Image);
            return Ok(addedFileId);
        }

        [HttpGet("{fileId}")]
        [EnableCors]
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
        [EnableCors]
        public async Task<IActionResult> RemoveFileFromReceipe(Guid receipeId, Guid fileId)
        {
            await _receipeImageRemover.RemoveImageFromReceipe(fileId, receipeId);
            return Ok();
        }

        [HttpDelete]
        [EnableCors]
        public async Task<IActionResult> RemoveFile([FromBody] IEnumerable<Guid> filesIds)
        {
            await _receipeImageRemover.RemoveImagesByIds(filesIds);
            return Ok();
        }
    }
}
