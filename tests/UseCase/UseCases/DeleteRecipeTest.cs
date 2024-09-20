using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using ProjectAspNet.Application.UseCases.Recipe;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Entities.Recipes;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public class DeleteRecipeTest
    {
        [Fact]
        public void Success()
        {
            var user = UserEntitieTest.Build();
            var recipe = RecipeEntitieTest.Build();

            var useCase = CreateUseCase(recipe);

            Func<Task> result = async () => await useCase.Execute(recipe.Id);

            result.Should().NotThrowAsync();
        }

        [Fact]
        public void No_Recipe_Error()
        {
            var recipe = RecipeEntitieTest.Build();

            var useCase = CreateUseCase(recipe);

            Func<Task> result = async () => await useCase.Execute(recipe.Id);

            result.Should().ThrowAsync<GetRecipeException>().Where(d => d.Errors.Contains(ResourceExceptMessages.NO_RECIPE_FOUND) && d.Errors.Count == 1);
        }

        public DeleteRecipeUseCase CreateUseCase(Recipe recipe)
        {
            var user = UserEntitieTest.Build();
            var unitOfWork = UnitOfWorkBuild.Build();
            var loggedUser = new LoggedUserBuilder().Build(user.user);
            var deleteRecipe = DeleteRecipeBuilder.Build();
            var getRecipe = new GetRecipeByIdBuilder().Build(user.user, recipe);

            return new DeleteRecipeUseCase(getRecipe, loggedUser, deleteRecipe, unitOfWork);
        }
    }
}
