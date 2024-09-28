using AutoMapper;
using ProjectAspNet.Application.UseCases.Repositories.Recipe;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Domain.Repositories.Recipes;
using ProjectAspNet.Domain.Repositories.Storage;
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
        private readonly IAzureStorageService _storageService;

        public RecipeDashboardUseCase(ILoggedUser loggedUser, IMapper mapper, IGetDashboardRecipe getRecipes, IAzureStorageService storageService)
        {
            _loggedUser = loggedUser;
            _mapper = mapper;
            _getRecipes = getRecipes;
            _storageService = storageService;
        }

        public async Task<GetRecipesResponse> Execute()
        {
            var user = await _loggedUser.getUser();

            var recipes = await _getRecipes.GetDashboardRecipe(user);

            return new GetRecipesResponse()
            {
                Recipe = await MapperListRecipes.GetRecipe(user, recipes, _storageService, _mapper)
            };
        }
    }
}
