using Conamitary.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Services.Abstract.Files
{
    public interface IFileAdder
    {
        Task<File> Add(File file);
    }
}
