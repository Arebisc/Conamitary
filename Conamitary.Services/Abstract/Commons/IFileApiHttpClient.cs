using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Services.Abstract.Commons
{
    public interface IFileApiHttpClient
    {
        Task InvokeDeleteFiles(IEnumerable<Guid> filesIds);
    }
}
