using Conamitary.Dtos.Receipes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conamitary.Services.Abstract.Receipe
{
    public interface IReceipeGetter
    {
        Task<ReceipeDto> Get(Guid recipeId);
        Task<IEnumerable<ReceipeDto>> Get();
    }
}
