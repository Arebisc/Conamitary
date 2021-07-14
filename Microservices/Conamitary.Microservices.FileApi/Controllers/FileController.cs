using Conamitary.Dtos.Files;
using Conamitary.Services.Abstract.Images;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Conamitary.Microservices.FileApi.Controllers
{
    [Route("api/file")]
    public class FileController : ControllerBase
    {
        private readonly IReceipeImageSaver _receipeImageSaver;

        public FileController(IReceipeImageSaver receipeImageSaver)
        {
            _receipeImageSaver = receipeImageSaver;
        }

        [HttpPost]
        public async Task<IActionResult> SaveFiles([FromForm] SaveReceipeImageDto saveFileContentDto)
        {
            var result = await _receipeImageSaver.Save(saveFileContentDto.ReceipeId, saveFileContentDto.File);
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
    }
}
