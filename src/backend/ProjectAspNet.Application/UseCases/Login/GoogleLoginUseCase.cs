using ProjectAspNet.Application.UseCases.Repositories.Login;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Repositories;
using ProjectAspNet.Domain.Repositories.Security.Tokens;
using ProjectAspNet.Domain.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.Login
{
    public class GoogleLoginUseCase : IGoogleLoginUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAdd _addUser;
        private readonly ITokenGenerator _generateToken;
        private readonly IUserByEmail _userByEmail;

        public GoogleLoginUseCase(IUnitOfWork unitOfWork, IUserAdd addUser, ITokenGenerator generateToken, IUserByEmail userByEmail)
        {
            _unitOfWork = unitOfWork;
            _addUser = addUser;
            _generateToken = generateToken;
            _userByEmail = userByEmail;
        }

        public async Task<string> Execute(string name, string email)
        {
            var user = await _userByEmail.UserByEmail(email);

            if(user is null)
            {
                user = new UserEntitie()
                {
                    Email = email,
                    Name = name,
                    Password = "-",
                };

                await _addUser.Add(user);
                await _unitOfWork.Commit();
            }

            return _generateToken.Generate(user.UserIdentifier);
        }
    }
}
