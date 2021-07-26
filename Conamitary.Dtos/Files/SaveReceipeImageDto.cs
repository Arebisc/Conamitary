using Microsoft.AspNetCore.Http;
using System;

namespace Conamitary.Dtos.Files
{
    public class SaveReceipeImageDto
    {
        public Guid ReceipeId { get; set; }
        public IFormFile Image { get; set; }
    }
}
