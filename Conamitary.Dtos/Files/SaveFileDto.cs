using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Dtos.Files
{
    public class SaveFileDto
    {
        public Guid FileId { get; set; }
        public IFormFile File { get; set; }
    }
}
