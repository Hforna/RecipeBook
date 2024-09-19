using AutoMapper;
using ProjectAspNet.Application.UseCases.Repositories.Recipe;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Domain.Repositories.Recipe;
using ProjectAspNet.Domain.Repositories.Users;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.Recipe
{
    public class GetRecipeUseCase : IGetRecipeUseCase
    {
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly IGetRecipeById _getRecipeById;

        public GetRecipeUseCase(IMapper mapper, ILoggedUser loggedUser, IGetRecipeById getRecipeById)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _getRecipeById = getRecipeById;
        }

        public async Task<ResponeGetRecipe> Execute(long recipeId)
        {
            var user = await _loggedUser.getUser();
            var recipes = await _getRecipeById.GetById(user, recipeId);

            if (recipes == null)
                throw new GetRecipeException(ResourceExceptMessages.NO_RECIPE_FOUND);

            return _mapper.Map<ResponeGetRecipe>(recipes);
        }
    }
}
