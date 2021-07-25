using Conamitary.Services.Commons.ServiceResults;
using System;
using System.Threading.Tasks;

namespace Conamitary.Services.Abstract.Receipe
{
    public interface IReceipeImageGetter
    {
        Task<FileGetterResult> Get(Guid fileId);
    }
}
