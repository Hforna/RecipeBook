using AutoMapper;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.User
{
    public class RegisterUserCase
    {
        private IMapper _mapper;

        public RegisterUserCase(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<RegisterUserResponse> Execute(RegisterUserRequest request)
        {
            Validate(request);

            var user = _mapper.Map<UserEntitie>(request);
            


            return new RegisterUserResponse() {Name = request.Name};
        }

        public async Task Validate(RegisterUserRequest request)
        {
            var validate = new UserRegisterValidate();
            var result = validate.Validate(request);

            if(result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new RegisterUserError(errorMessages);
            }
        }
    }
}
