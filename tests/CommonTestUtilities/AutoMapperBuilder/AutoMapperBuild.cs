using AutoMapper;
using Moq;
using ProjectAspNet.Application.Services.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.AutoMapperBuilder
{
    public class AutoMapperBuild
    {
        public static IMapper Build()
        {
            var mapper = new AutoMapper.MapperConfiguration(x => { x.AddProfile(new UserMappper()); x.AddProfile(new ProductMapper()); x.AddProfile(new ProfileMapper()); }).CreateMapper();
            return mapper;
        }
    }
}
