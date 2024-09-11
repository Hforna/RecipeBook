using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTestUtilities;
using CommonTestUtilities.AutoMapperBuilder;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.User;
using FluentAssertions;
using ProjectAspNet.Application.UseCases.User;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Exceptions.Exceptions;

namespace UseCases
{
    public class UserCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RegisterUserRequestBuilder.Create();
            var useCase = CreateUseCase(request, false);
            var result = await useCase.Execute(request);

            result.Name.Should().Be(request.Name);

        }

        [Fact]
        public async Task Error_Email_Exists()
        {
            var request = RegisterUserRequestBuilder.Create();
            var useCase = CreateUseCase(request, true);
            //var result = await useCase.Execute(request);

            await Assert.ThrowsAsync<RegisterUserError>(() => useCase.Execute(request));
            
        }

        public RegisterUserCase CreateUseCase(RegisterUserRequest request, bool emailResult)
        {
            var mapper = AutoMapperBuild.Build();
            var unitOfWork = UnitOfWorkBuild.Build();
            var cryptography = CryptographyBuild.Build();
            var userAdd = UserAddBuild.Build();
            var userEmailExists = new UserEmailExistsBuild();

            if (request.Email is not null)
                userEmailExists.EmailExists(request.Email, emailResult);

            return new RegisterUserCase(mapper, cryptography, unitOfWork, userAdd, userEmailExists.Build());

        }
    }
}
