using Conamitary.Database;
using Conamitary.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Services.Abstract.Receipe
{
    public interface IReceipeRemover
    {
        Task<ReceipeDto> Remove(Guid receipeId);
    }
}
