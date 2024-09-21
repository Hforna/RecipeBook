using AutoMapper;
using ProjectAspNet.Application.UseCases.Repositories.Recipe;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Domain.Repositories.Recipes;
using ProjectAspNet.Domain.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.Recipe
{
    public class RecipeDashboardUseCase : IRecipeDashboardUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IMapper _mapper;
        private readonly IGetDashboardRecipe _getRecipes;

        public RecipeDashboardUseCase(ILoggedUser loggedUser, IMapper mapper, IGetDashboardRecipe getRecipes)
        {
            _loggedUser = loggedUser;
            _mapper = mapper;
            _getRecipes = getRecipes;
        }

        public async Task<GetRecipesResponse> Execute()
        {
            var user = await _loggedUser.getUser();

            var recipes = await _getRecipes.GetDashboardRecipe(user);

            return new GetRecipesResponse()
            {
                Recipe = _mapper.Map<IList<RecipeResponseJson>>(recipes)
            };
        }
    }
}
