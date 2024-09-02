using CommonTestUtilities.AutoMapperBuilder;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.User;
using FluentAssertions;
using ProjectAspNet.Application.UseCases.User;
using ProjectAspNet.Domain.Entities;

namespace UseCase
{
    public class UserCase
    {
        [Fact]
        public async Task Success()
        {
            var cryptoObject = CryptographyBuild.Build();
            var mapper = UserMapperBuild.Build();
            var unitOfWork = UnitOfWorkBuild.Build();
            var userAdd = UserAddBuild.Build();
            var userEmailExists = new UserEmailExistsBuild().Build();
            var request = RegisterUserRequestBuilder.Create();
            var userCase = new RegisterUserCase(mapper, cryptoObject, unitOfWork, userAdd, userEmailExists);
            var result = await userCase.Execute(request);

            result.Name.Should().Be(request.Name);
        }
    }
}