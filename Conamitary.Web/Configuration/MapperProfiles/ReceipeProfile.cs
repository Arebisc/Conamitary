using AutoMapper;
using Conamitary.Database.Models;
using Conamitary.Dtos;
using Conamitary.Dtos.Receipes;
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

            CreateMap<AddReceipeDto, Receipe>()
                .ForMember(x => x.Images, opts => opts.Ignore());
        }
    }
}
