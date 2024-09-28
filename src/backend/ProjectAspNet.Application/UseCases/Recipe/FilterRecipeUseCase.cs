using AutoMapper;
using ProjectAspNet.Application.UseCases.Repositories.Recipe;
using ProjectAspNet.Application.Validators.Recipe;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Domain.DTOs;
using ProjectAspNet.Domain.Enums;
using ProjectAspNet.Domain.Repositories.Recipe;
using ProjectAspNet.Domain.Repositories.Storage;
using ProjectAspNet.Domain.Repositories.Users;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.Recipe
{
    public class FilterRecipeUseCase : IFilterRecipeUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IFilterRecipe _filter;
        private readonly IMapper _mapper;
        private readonly IAzureStorageService _storageService;

        public FilterRecipeUseCase(ILoggedUser loggedUser, IFilterRecipe filter, IMapper mapper, IAzureStorageService storageService)
        {
            _loggedUser = loggedUser;
            _filter = filter;
            _mapper = mapper;
            _storageService = storageService;
        }


        public async Task<GetRecipesResponse> Execute(RequestFilterRecipe request)
        {
            Validate(request);

            var user = await _loggedUser.getUser();

            var filter = new FilterRecipeDto()
            {
                DishTypes = request.DishTypes.Distinct().Select(c => (DishType)c).ToList(),
                Difficulty = request.Difficulty.Distinct().Select(c => (Difficulty)c).ToList(),
                CookingTime = request.CookingTime.Distinct().Select(c => (CookingTime)c).ToList(),
                TitleIngredientsRecipe = request.TitleIngredientsRecipe
            };

            var recipes = await _filter.FilterRecipe(user, filter);

            return new GetRecipesResponse()
            {
                Recipe = await MapperListRecipes.GetRecipe(user, recipes, _storageService, _mapper)
            };
        }

        public void Validate(RequestFilterRecipe request)
        {
            var validate = new RecipeFilterValidator();
            var result = validate.Validate(request);

            if(result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new FilterRecipeException(errorMessages);
            }
        }
    }
}
