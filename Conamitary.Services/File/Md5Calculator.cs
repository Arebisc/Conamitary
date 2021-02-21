using Conamitary.Services.Abstract.File;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Conamitary.Services.File
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
