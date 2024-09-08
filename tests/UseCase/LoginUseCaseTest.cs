using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using ProjectAspNet.Application.Services.Cryptography;
using ProjectAspNet.Application.UseCases.User;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Domain.Entities;

namespace UseCase
{
    public class LoginUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var user = UserEntitieTest.Build();
            var useCase = LoginUseCase(user.Item1);
            var response = await useCase.Execute(new LoginUserRequest
            {
                Email = user.Item1.Email,
                Password = user.Item2
            });

            response.Name.Should().NotBeNullOrEmpty();
        }

        public LoginUserCase LoginUseCase(UserEntitie user)
        {
            var passwordCrypto = CryptographyBuild.Build();
            var userExists = new UserEmailExistsBuild();

            if (user is not null)
                userExists.Password_Email_Exists(user);

            return new LoginUserCase(userExists.Build(), passwordCrypto);


        }
    }
}
