using ProjectAspNet.Application.UseCases.Repositories.Token;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Domain.Entities.Tokens;
using ProjectAspNet.Domain.Repositories;
using ProjectAspNet.Domain.Repositories.Security.Tokens;
using ProjectAspNet.Domain.Repositories.Users;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.Token
{
    public class RefreshTokenUseCase : IRefreshTokenUseCase
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGenerator _tokenGenerator;

        public RefreshTokenUseCase(IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork, ITokenGenerator tokenGenerator)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<TokenResponse> Execute(RequestRefreshToken request)
        {
            var refreshToken = await _refreshTokenRepository.Get(request.RefreshToken);

            if (refreshToken is null)
                throw new RefreshTokenException("Wrong refresh token");

            var timeExpires = refreshToken.CreatedOn.AddDays(7);
            if (DateTime.Compare(timeExpires, DateTime.UtcNow) < 0)
                throw new RefreshTokenException("Token expired");

            var token = new RefreshTokenEntitie()
            {
                UserId = refreshToken.UserId,                
            };

            await _refreshTokenRepository.SaveRefreshToken(token, refreshToken.User);
            await _unitOfWork.Commit();

            return new TokenResponse() { RefreshToken = token.Value, AccessToken = _tokenGenerator.Generate(refreshToken.User.UserIdentifier) };
        }
    }
}
