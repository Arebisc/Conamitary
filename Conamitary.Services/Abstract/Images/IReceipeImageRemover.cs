using Conamitary.Services.Commons.ServiceResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Services.Abstract.Images
{
    public interface IReceipeImageRemover
    {
        Task<FileRemoverResult> Remove(Guid fileId);
    }
}
