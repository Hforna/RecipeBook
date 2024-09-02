using CommonTestUtilities.AutoMapperBuilder;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.User;
using FluentAssertions;
using ProjectAspNet.Application.UseCases.User;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Exceptions.Exceptions;

namespace UseCase
{
    public class UserCase
    {
        [Fact]
        public async Task Success()
        {
            var request = RegisterUserRequestBuilder.Create();
            var userCase = CreateUseCase();
            var result = await userCase.Execute(request);

            result.Name.Should().Be(request.Name);
        }

        [Fact]
        public async Task Error_email_exists()
        {
            var request = RegisterUserRequestBuilder.Create();
            var userCase = CreateUseCase(request.Email);

            Func<Task> result = async () => await userCase.Execute(request);

            await result.Should().ThrowAsync<RegisterUserError>().Where(e => e.Errors.Count == 1 && e.Errors.Contains("This e-mail already exists"));
        }

        public RegisterUserCase CreateUseCase(string? email = null)
        {
            var cryptoObject = CryptographyBuild.Build();
            var mapper = UserMapperBuild.Build();
            var unitOfWork = UnitOfWorkBuild.Build();
            var userAdd = UserAddBuild.Build();
            var userEmailExists = new UserEmailExistsBuild();

            if(string.IsNullOrEmpty(email) == false)
            {
                userEmailExists.EmailExists(email);
            }

            return new RegisterUserCase(mapper, cryptoObject, unitOfWork, userAdd, userEmailExists.Build());
        }
    }
}