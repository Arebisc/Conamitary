using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conamitary.Services.Abstract.Receipe
{
    public interface IReceipeFileAdder
    {
        Task Add(Guid receipeId, IEnumerable<IFormFile> files);
    }
}
