using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Services.Commons.ServiceResults
{
    public enum FileRemoverResult
    {
        Ok = 0,
        StillInUse = 1,
        FileNotFound = 2,
        Error = 3
    }
}
