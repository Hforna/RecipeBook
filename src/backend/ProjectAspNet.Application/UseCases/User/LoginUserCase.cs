using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectAspNet.Domain.Repositories.Users;
using ProjectAspNet.Exceptions.Exceptions;
using ProjectAspNet.Domain.Repositories.Security.Tokens;
using ProjectAspNet.Domain.Repositories.Security;
using ProjectAspNet.Application.UseCases.RepositoriesUseCases.User;

namespace ProjectAspNet.Application.UseCases.User
{
    public class LoginUserCase : ILoginUser
    {
        private readonly IUserEmailExists _userExists;
        private readonly ICryptography _passwordCryptography;
        private readonly ITokenGenerator _tokenGenerator;

        public LoginUserCase(IUserEmailExists userExists, ICryptography passwordCryptography, ITokenGenerator tokenGenerator)
        {
            _userExists = userExists;
            _passwordCryptography = passwordCryptography;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<RegisterUserResponse> Execute(LoginUserRequest request)
        {
            var encryptPassword = _passwordCryptography.Encrypt(request.Password!);
            var user = await _userExists.LoginByEmailAndPassword(request.Email!, encryptPassword);

            if (user is null)
            {
                throw new LoginUserException();
            }

            return new RegisterUserResponse { Name = user.Name, Token = new TokenResponse() { TokenGenerated = _tokenGenerator.Generate(user.UserIdentifier) } };
        }
    }
}
