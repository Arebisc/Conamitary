using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Dtos.Files
{
    public class FileDto
    {
        public Guid Id { get; set; }
        public string Md5Checksum { get; set; }
    }
}
