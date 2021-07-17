using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Services.Commons.ServiceResults
{
    public class FileGetterResult
    {
        public Stream Stream { get; set; }
        public string ContentType { get; set; }
    }
}
