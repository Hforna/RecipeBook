using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.User
{
    public interface IUserCase
    {
        public Task<RegisterUserResponse> Execute(RegisterUserRequest request);
    }
}
