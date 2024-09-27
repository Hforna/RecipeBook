using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Http;
using ProjectAspNet.Application.UseCases.Repositories.Recipe;
using ProjectAspNet.Domain.Repositories;
using ProjectAspNet.Domain.Repositories.Recipe;
using ProjectAspNet.Domain.Repositories.Recipes;
using ProjectAspNet.Domain.Repositories.Storage;
using ProjectAspNet.Domain.Repositories.Users;
using ProjectAspNet.Exceptions.Exceptions;
using ProjectAspNet.Infrastructure.Storage;
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
        private readonly IUpdateRecipe _updateRecipe;
        private readonly IAzureStorageService _storageService;

        public UpdateImageRecipeUseCase(IGetRecipeById getRecipeById, ILoggedUser loggedUser, IUnitOfWork unitOfWork, IUpdateRecipe updateRecipe, IAzureStorageService storageService)
        {
            _recipeById = getRecipeById;
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
            _updateRecipe = updateRecipe;
            _storageService = storageService;
        }

        public async Task Execute(IFormFile file, long id)
        {
            var user = await _loggedUser.getUser();

            var recipe = await _recipeById.GetById(user, id);

            if (recipe is null)
                throw new GetRecipeException(ResourceExceptMessages.NO_RECIPE_FOUND);

            var readFile = file.OpenReadStream();

            if (readFile.Is<JointPhotographicExpertsGroup>() == false || readFile.Is<PortableNetworkGraphic>() == false)
                throw new GetRecipeException(ResourceExceptMessages.FORMAT_IMAGE_WRONG);

            readFile.Position = 0;

            recipe.ImageIdentifier = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            _updateRecipe.Update(recipe);
            await _unitOfWork.Commit();

            await _storageService.Upload(user, readFile, recipe.ImageIdentifier);
        }
    }
}
