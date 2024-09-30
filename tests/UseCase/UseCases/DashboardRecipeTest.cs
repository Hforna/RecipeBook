using CommonTestUtilities.AutoMapperBuilder;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using ProjectAspNet.Application.UseCases.Recipe;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Entities.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public class DashboardRecipeTest
    {
        [Fact]
        public async Task Success()
        {
            var user = UserEntitieTest.Build();
            var recipes = RecipesListTest.Build(user.user.Id, 4);

            var useCase = CreateUseCase(user.user, recipes);

            Func<Task> result = async () => await useCase.Execute();

            await result.Should().NotThrowAsync();
        }

        public RecipeDashboardUseCase CreateUseCase(UserEntitie user, IList<Recipe> recipes)
        {
            var mapper = AutoMapperBuild.Build();
            var loggedUser = new LoggedUserBuilder().Build(user);
            var DashBoardRecipe = new GetDashBoardRecipe().Build(user, recipes);
            var storageService = new AzureStorageServiceBuilder();

            return new RecipeDashboardUseCase(loggedUser, mapper, DashBoardRecipe, storageService.Build());
        }
    }
}
