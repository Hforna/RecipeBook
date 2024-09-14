using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.User;
using FluentAssertions;
using ProjectAspNet.Application.UseCases.User;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Infrastructure.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public class ChangePasswordUseCaseTest
    {
        [Fact]
        public void Success()
        {
            var user = UserEntitieTest.Build();
            var request = ChangePasswordRequestBuilder.Build();
            request.Password = user.password;
            var useCase = CreateUseCase(user.user);
            var result = useCase.Execute(request);
            var cryptoNew = CryptographyBuild.Build();
            user.user.Password.Should().Be(cryptoNew.Encrypt(request.NewPassword));
        }

        public ChangePasswordUseCase CreateUseCase(UserEntitie user)
        {
            var unitOfWork = UnitOfWorkBuild.Build();
            var userTrack = new getUserTrackingBuilder();
            var userUpdate = UpdateUserBuilder.Build();
            var loggedUser = new LoggedUserBuilder();
            var passwordCrypto = CryptographyBuild.Build();
            return new ChangePasswordUseCase(unitOfWork, userTrack.Build(user), userUpdate, loggedUser.Build(user), passwordCrypto);
        }
    }
}
