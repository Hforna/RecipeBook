using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Request.User;
using FluentAssertions;
using ProjectAspNet.Application.UseCases.User;

namespace UseCase
{
    public class UserCase
    {
        [Fact]
        public async Task Success()
        {
            var cryptoObject = CryptographyBuild.Build();
            var request = RegisterUserRequestBuilder.Create();
            var userCase = new RegisterUserCase(cryptoObject);
            var result = await userCase.Execute(request);

            result.Name.Should().Be(request.Name);
        }
    }
}