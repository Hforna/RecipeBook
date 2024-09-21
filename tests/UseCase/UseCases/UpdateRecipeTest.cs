using CommonTestUtilities.AutoMapperBuilder;
using CommonTestUtilities.Repositories;
using ProjectAspNet.Application.UseCases.Repositories.Recipe;
using ProjectAspNet.Application.UseCases.Recipe;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Request.Recipe;
using FluentAssertions;
using ProjectAspNet.Domain.Entities.Recipes;
using ProjectAspNet.Exceptions.Exceptions;

namespace UseCases
{
    public class UpdateRecipeTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestRecipeBuild.Build();
            var user = UserEntitieTest.Build();
            var recipe = RecipeEntitieTest.Build();

            var useCase = CreateUseCase(user.user, recipe);
            Func<Task> result = async () => await useCase.Execute(recipe.Id, request);

            await result.Should().NotThrowAsync();
        }
        
        [Fact]
        public async Task Error_No_Recipe()
        {
            var request = RequestRecipeBuild.Build();
            var user = UserEntitieTest.Build();
            var recipe = RecipeEntitieTest.Build();

            var useCase = CreateUseCase(user.user, recipe);
            Func<Task> result = async () => await useCase.Execute(23, request);

            await result.Should().ThrowAsync<GetRecipeException>().Where(e => e.Errors.Contains(ResourceExceptMessages.NO_RECIPE_FOUND) && e.Errors.Count == 1);
        }

        public IUpdateRecipeUseCase CreateUseCase(UserEntitie user, Recipe recipe)
        {
            var mapper = AutoMapperBuild.Build();
            var loggedUser = new LoggedUserBuilder().Build(user);
            var unitOfWork = UnitOfWorkBuild.Build();
            var updateRecipe = UpdateRecipeBuilder.Build(user, recipe);

            return new UpdateRecipeUseCase(updateRecipe, loggedUser, unitOfWork, mapper);
        }
    }
}
