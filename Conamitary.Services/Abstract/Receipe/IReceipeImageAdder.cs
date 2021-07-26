using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Conamitary.Services.Abstract.Receipe
{
    public interface IReceipeImageAdder
    {
        Task<Guid> Add(Guid receipeId, IFormFile file);
    }
}
