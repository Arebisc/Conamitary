﻿using Conamitary.Database;
using Conamitary.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Services.Abstract.Receipe
{
    public interface IReceipeUpdater
    {
        Task<ReceipeDto> Update(Guid receipeId, ReceipeDto receipeDto);
    }
}
