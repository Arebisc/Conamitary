using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Conamitary.Dtos.Files
{
    public class AddImagesToReceipeDto
    {
        public Guid ReceipeId { get; set; }
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
