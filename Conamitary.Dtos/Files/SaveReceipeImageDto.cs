using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Dtos.Files
{
    public class SaveReceipeImageDto
    {
        public Guid ReceipeId { get; set; }
        public IFormFile File { get; set; }
    }
}
