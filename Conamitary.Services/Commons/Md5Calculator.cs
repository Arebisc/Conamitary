using Conamitary.Services.Abstract.Commons;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Conamitary.Services.Commons
{
    public class Md5Calculator : IMd5Calculator
    {
        public string CalculateHash(Stream stream)
        {
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            using (var md5 = MD5.Create())
            {
                var bytes = md5.ComputeHash(memoryStream.ToArray());
                return BitConverter.ToString(bytes).Replace("-", string.Empty).ToLower();
            }
        }
    }
}
