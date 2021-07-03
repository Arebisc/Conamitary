using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Services.Commons.ServiceResults
{
    public enum FileSaverResultEnum
    {
        Ok = 0,
        AlreadyExists = 1,
        Error = 2,
        ReceipeDoesNotExist = 3
    }
}
