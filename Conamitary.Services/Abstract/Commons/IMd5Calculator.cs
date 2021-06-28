using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Services.Abstract.Commons
{
    public interface IMd5Calculator
    {
        public string CalculateHash(Stream stream);
    }
}
