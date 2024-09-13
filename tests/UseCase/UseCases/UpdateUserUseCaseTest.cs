using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.User;
using FluentAssertions;
using ProjectAspNet.Application.UseCases.User;
using ProjectAspNet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public class UpdateUserUseCaseTest
    {
        [Fact]
        public void Success()
        {
            var request = UpdateUserRequestBuilder.Build();
            var user = UserEntitieTest.Build();
            var useCase = CreateUseCase(user.user);
            var result = useCase.Execute(request);
            user.user.Name.Should().Be(request.Name);
            user.user.Email.Should().Be(request.Email);
        }

        public UpdateUserUseCase CreateUseCase(UserEntitie user)
        {
            var loggedUser = new LoggedUserBuilder();
            var unitOfWork = UnitOfWorkBuild.Build();
            var userEmailExists = new UserEmailExistsBuild();
            var dbUpdate = UpdateUserBuilder.Build();
            var userTracking = new getUserTrackingBuilder().Build(user);
            var useCase = new UpdateUserUseCase(unitOfWork, loggedUser.Build(user), userTracking, dbUpdate, userEmailExists.Build());
            return useCase;
        }
    }
}
