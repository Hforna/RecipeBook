using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Http;
using ProjectAspNet.Application.UseCases.Repositories.Recipe;
using ProjectAspNet.Domain.Repositories;
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
    public class UpdateImageRecipeUseCase : IUpdateImageRecipe
    {
        private readonly IGetRecipeById _recipeById;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateImageRecipeUseCase(IGetRecipeById getRecipeById, ILoggedUser loggedUser, IUnitOfWork unitOfWork)
        {
            _recipeById = getRecipeById;
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(IFormFile file, long id)
        {
            var user = await _loggedUser.getUser();
            var recipe = _recipeById.GetById(user, id);

            if (recipe is null)
                throw new GetRecipeException(ResourceExceptMessages.NO_RECIPE_FOUND);

            var readFile = file.OpenReadStream();

            if (readFile.Is<JointPhotographicExpertsGroup>() == false && readFile.Is<PortableNetworkGraphic>() == false)
                throw new CreateRecipeException(ResourceExceptMessages.FORMAT_IMAGE_WRONG);
        }
    }
}
