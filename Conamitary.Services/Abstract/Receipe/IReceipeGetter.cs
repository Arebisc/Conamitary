using Conamitary.Database;
using Conamitary.Dtos;
using Conamitary.Dtos.Receipes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Services.Abstract.Receipe
{
    public interface IReceipeGetter
    {
        Task<ReceipeDto> Get(Guid recipeId);
        Task<IEnumerable<ReceipeDto>> Get();
    }
}
