using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conamitary.Web.Controllers
{
    [Route("api/receipe")]
    public class ReceipeController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
