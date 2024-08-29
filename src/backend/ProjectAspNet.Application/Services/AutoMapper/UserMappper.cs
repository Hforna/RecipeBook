using AutoMapper;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.Services.AutoMapper
{
    public class UserMappper : Profile
    {
        public UserMappper()
        {
            CreateMap<RegisterUserRequest, UserEntitie>().ForMember(x => x.Password, opt => opt.Ignore());
        }
    }
}
