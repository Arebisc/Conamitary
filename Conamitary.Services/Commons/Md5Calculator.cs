using Conamitary.Services.Abstract.Commons;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Conamitary.Services.Commons
{
    public class Md5Calculator : IMd5Calculator
    {
        public string CalculateHash(Stream stream)
        {
            using (var md5 = MD5.Create())
            {
                return Encoding.Default.GetString(md5.ComputeHash(stream));
            }
        }
    }
}
