using AutoMapper;
using Moq;
using ProjectAspNet.Application.Services.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.AutoMapper
{
    public class UserMapperBuild
    {
        public static UserMappper Build()
        {
            var mock = new Mock<IMapper>();

            return mock.Object;
        }
    }
}
