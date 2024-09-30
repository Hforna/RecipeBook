using CommonTestUtilities.AutoMapperBuilder;
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
    public class GetRecipeByIdTest
    {
        [Fact]
        public async Task Success()
        {
            var recipe = RecipeEntitieTest.Build();
            var user = UserEntitieTest.Build();
            var useCase = CreateRecipe(user.user, recipe);
            var result = await useCase.Execute(recipe.Id);
            result.Title.Should().Be(recipe.Title);
            result.TimeRecipe.Should().Be((ProjectAspNet.Communication.Requests.Enums.CookingTime)recipe.TimeRecipe!);
        }
        
        [Fact]
        public async Task Error_No_Recipe()
        {
            var recipe = RecipeEntitieTest.Build();
            var user = UserEntitieTest.Build();
            var useCase = CreateRecipe(user.user, recipe);
            Func<Task> result = async () => { await useCase.Execute(24); };
            await result.Should().ThrowAsync<GetRecipeException>().Where(d => d.Errors.Count() == 1 && d.Errors.Contains(ResourceExceptMessages.NO_RECIPE_FOUND));            
        }

        public GetRecipeUseCase CreateRecipe(UserEntitie user, Recipe recipe)
        {
            var mapper = AutoMapperBuild.Build();
            var loggedUser = new LoggedUserBuilder().Build(user);
            var getRecipeById = new GetRecipeByIdBuilder().Build(user, recipe);
            var storageService = new AzureStorageServiceBuilder();
            return new GetRecipeUseCase(mapper, loggedUser, getRecipeById, storageService.Build());
        }
    }
}
