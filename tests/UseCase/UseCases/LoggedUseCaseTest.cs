using CommonTestUtilities.AutoMapperBuilder;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using ProjectAspNet.Application.UseCases.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public class LoggedUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedBuild = new LoggedUserBuilder();
            var user = UserEntitieTest.Build();
            var mapper = AutoMapperBuild.Build();
            var useCase = new GetUserProfileUseCase(loggedBuild.Build(user.user), mapper);
            var result = await useCase.Execute();
            result.Name.Should().Be(user.user.Name);
        }
    }
}
