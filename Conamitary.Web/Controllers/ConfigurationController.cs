using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Conamitary.Web.Controllers
{
    [Route("api/configuration")]
    public class ConfigurationController : ControllerBase
    {
        private readonly string _fileApiUrl;

        public ConfigurationController(IConfiguration configuration)
        {
            _fileApiUrl = configuration.GetSection("FileApiUrl").Value;
        }

        [HttpGet("fileApiUrl")]
        public IActionResult Get()
        {
            return Ok(_fileApiUrl);
        }
    }
}
