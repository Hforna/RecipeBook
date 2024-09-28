using AutoMapper;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Repositories.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.Recipe
{
    public static class MapperListRecipes
    {
        public static async Task<IList<RecipeResponseJson>> GetRecipe(UserEntitie user, IList<ProjectAspNet.Domain.Entities.Recipes.Recipe> recipes, IAzureStorageService storageService, IMapper mapper)
        {
            var response = recipes.Select(async recipe =>
            {
                var recipeMap = mapper.Map<RecipeResponseJson>(recipe);

                if (recipe.ImageIdentifier is not null)
                    recipeMap.ImageUrl = await storageService.GetFileUrl(user, recipe.ImageIdentifier!);

                return recipeMap;
            });

            var responseTask = await Task.WhenAll(response);

            return responseTask;
        }
    }
}
