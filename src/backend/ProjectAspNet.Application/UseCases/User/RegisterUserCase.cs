using AutoMapper;
using ProjectAspNet.Application.Services.Cryptography;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Repositories;
using ProjectAspNet.Domain.Repositories.Users;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.User
{
    public class RegisterUserCase : IUserCase
    {
        private IMapper _mapper;
        private PasswordCryptography _password;
        private IUnitOfWork _unitOfWork;
        private IUserAdd _userAdd;
        private IUserEmailExists _userEmailExists;

        public RegisterUserCase(IMapper mapper, PasswordCryptography password, IUnitOfWork unitOfWork, IUserAdd userAdd, IUserEmailExists userEmailExists)
        {
            _mapper = mapper;
            _password = password;
            _unitOfWork = unitOfWork;
            _userAdd = userAdd;
            _userEmailExists = userEmailExists;
        }

        public async Task<RegisterUserResponse> Execute(RegisterUserRequest request)
        {
            await Validate(request);

            var user = _mapper.Map<UserEntitie>(request);
            user.Password = _password.Encrypt(request.Password);
            await _userAdd.Add(user);
            await _unitOfWork.Commit();

            return new RegisterUserResponse() {Name = request.Name};
        }

        public async Task Validate(RegisterUserRequest request)
        {
            var validate = new UserRegisterValidate();
            var result = validate.Validate(request);
            if(await _userEmailExists.EmailExists(request.Email))
            {
                throw new RegisterUserError("This e-mail already exists");
            }

            if(result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new RegisterUserError(errorMessages);
            }
        }
    }
}
