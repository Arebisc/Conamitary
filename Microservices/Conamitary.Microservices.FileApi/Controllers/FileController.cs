using Conamitary.Dtos.Files;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conamitary.Microservices.FileApi.Controllers
{
    [Route("api/file")]
    public class FileController : ControllerBase
    {
        public Task SaveFiles([FromForm] SaveFilesDto files)
        {
            return Task.CompletedTask;
        }
    }
}
