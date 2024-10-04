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
using ProjectAspNet.Domain.Entities.Tokens;
using ProjectAspNet.Domain.Repositories;

namespace ProjectAspNet.Application.UseCases.User
{
    public class LoginUserCase : ILoginUser
    {
        private readonly IUserEmailExists _userExists;
        private readonly ICryptography _passwordCryptography;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LoginUserCase(IUserEmailExists userExists, ICryptography passwordCryptography, 
            ITokenGenerator tokenGenerator, IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork)
        {
            _userExists = userExists;
            _passwordCryptography = passwordCryptography;
            _tokenGenerator = tokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RegisterUserResponse> Execute(LoginUserRequest request)
        {
            var user = await _userExists.LoginByEmail(request.Email!);

            if (user is null || _passwordCryptography.IsValid(request.Password!, user.Password) == false)
                throw new LoginUserException();

            var refreshToken = new RefreshTokenEntitie()
            {
                UserId = user.Id,
            };

            if (await _refreshTokenRepository.TokenExists(user))
            {
                await _refreshTokenRepository.SaveRefreshToken(refreshToken, user);
            } else
            {
                await _refreshTokenRepository.AddRefreshToken(refreshToken);
            }
            await _unitOfWork.Commit();

            return new RegisterUserResponse { Name = user.Name, Token = new TokenResponse() { AccessToken = _tokenGenerator.Generate(user.UserIdentifier),
                RefreshToken = refreshToken.Value } };
        }
    }
}
