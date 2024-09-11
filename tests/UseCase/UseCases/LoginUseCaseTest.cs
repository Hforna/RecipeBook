using CommonTestUtilities.AutoMapperBuilder;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using ProjectAspNet.Application.UseCases.User;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public class LoginUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var user = UserEntitieTest.Build();
            var useCase = CreateLoginUseCase(user.user, false);
            var result = await useCase.Execute(new LoginUserRequest()
            {
                Email = user.user.Email,
                Password = user.password
            });
            result.Name.Should().Be(user.user.Name);
        }

        [Fact]
        public async Task ErrorPasswordEmailExists()
        {
            var user = UserEntitieTest.Build();
            var useCase = CreateLoginUseCase(user.user, true);
            var result = useCase.Execute(new LoginUserRequest()
            {
                Email = user.user.Email,
                Password = user.password
            });

            await Assert.ThrowsAsync<LoginUserException>(() => result);

        }

        public static LoginUserCase CreateLoginUseCase(UserEntitie user, bool exists)
        {
            var EmailExists = new UserEmailExistsBuild();
            var Cryptography = CryptographyBuild.Build();
            if (user is not null)
                EmailExists.Password_Email_Exists(user, exists);

            return new LoginUserCase(EmailExists.Build(), Cryptography);
        }
    }
}
