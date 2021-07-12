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
            CreateMap<Receipe, ReceipeDto>()
                .ForMember(x => x.ImagesIds, opts => opts.MapFrom(y => y.Images.Select(i => i.Id)));
            CreateMap<ReceipeDto, Receipe>()
                .ForMember(x => x.Images, opts => opts.Ignore());

            CreateMap<AddReceipeDto, Receipe>()
                .ForMember(x => x.Images, opts => opts.Ignore());
        }
    }
}
