using AutoMapper;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.Services.AutoMapper
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<UserEntitie, ResponseUserProfile>();
        }
    }
}
