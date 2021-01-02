using AutoMapper;
using Conamitary.Database.Models;
using Conamitary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conamitary.Web.Configuration.MapperProfiles
{
    public class ReceipeProfile : Profile
    {
        public ReceipeProfile()
        {
            CreateMap<Receipe, ReceipeDto>();
            CreateMap<ReceipeDto, Receipe>();
        }
    }
}
