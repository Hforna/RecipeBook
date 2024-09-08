using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectAspNet.Application.Services.Cryptography;
using ProjectAspNet.Domain.Repositories.Users;
using ProjectAspNet.Exceptions.Exceptions;

namespace ProjectAspNet.Application.UseCases.User
{
    public class LoginUserCase : ILoginUser
    {
        private readonly IUserEmailExists _userExists;
        private readonly PasswordCryptography _passwordCryptography;

        public LoginUserCase(IUserEmailExists userExists, PasswordCryptography passwordCryptography)
        {
            _userExists = userExists;
            _passwordCryptography = passwordCryptography;
        }

        public async Task<RegisterUserResponse> Execute(LoginUserRequest request)
        {
            var encryptPassword = _passwordCryptography.Encrypt(request.Password);
            var user = await _userExists.LoginByEmailAndPassword(request.Email, encryptPassword);

            if (user is null)
            {
                throw new LoginUserException();
            }

            return new RegisterUserResponse { Name = user.Name };
        }
    }
}
