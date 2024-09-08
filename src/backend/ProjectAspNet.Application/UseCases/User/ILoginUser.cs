using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;

namespace ProjectAspNet.Application.UseCases.User
{
    public interface ILoginUser
    {
        public Task<RegisterUserResponse> Execute(LoginUserRequest request);
    }
}
