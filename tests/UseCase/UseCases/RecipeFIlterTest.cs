using CommonTestUtilities.AutoMapperBuilder;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.Recipe;
using FluentAssertions;
using ProjectAspNet.Application.UseCases.Recipe;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Domain.DTOs;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Entities.Recipes;
using ProjectAspNet.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public class RecipeFIlterTest
    {
        [Fact]
        public async Task Success()
        {
            var recipe = RecipeEntitieTest.Build();
            var listRecipe = new List<Recipe>();
            listRecipe.Add(recipe);
            var user = UserEntitieTest.Build();
            var request = RequestFilterRecipeBuild.Build();
            request.TitleIngredientsRecipe = recipe.Title;
            var useCase = CreateFilter(user.user, request, listRecipe);
            var result = await useCase.Execute(request);

            result.Recipe.Should().NotBeNull();
        }

        public FilterRecipeUseCase CreateFilter(UserEntitie user, RequestFilterRecipe request, IList<Recipe> recipe)
        {
            var mapper = AutoMapperBuild.Build();
            var loggedUser = new LoggedUserBuilder().Build(user);
            var dto = new FilterRecipeDto()
            {
                DishTypes = request.DishTypes.Distinct().Select(c => (DishType)c).ToList(),
                Difficulty = request.Difficulty.Distinct().Select(c => (Difficulty)c).ToList(),
                CookingTime = request.CookingTime.Distinct().Select(c => (CookingTime)c).ToList(),
                TitleIngredientsRecipe = request.TitleIngredientsRecipe
            };
            var storageService = new AzureStorageServiceBuilder();
            var filterRecipe = new RecipeFilterBUilder().Build(user, dto, recipe);
            storageService.GetFileUrlMock(user, "dd.jpg");
            return new FilterRecipeUseCase(loggedUser, filterRecipe, mapper, storageService.Build());
        }
    }
}
