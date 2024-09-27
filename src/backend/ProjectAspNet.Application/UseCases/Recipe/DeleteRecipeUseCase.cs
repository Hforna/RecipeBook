using ProjectAspNet.Application.UseCases.Repositories.Recipe;
using ProjectAspNet.Domain.Repositories;
using ProjectAspNet.Domain.Repositories.Recipe;
using ProjectAspNet.Domain.Repositories.Recipes;
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
    public class DeleteRecipeUseCase : IDeleteRecipe
    {
        private readonly IGetRecipeById _getRecipe;
        private readonly ILoggedUser _loggedUser;
        private readonly IDeleteRecipeById _deleteRecipe;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAzureStorageService _storageService;
        public DeleteRecipeUseCase(IGetRecipeById getRecipe, ILoggedUser loggedUser, IDeleteRecipeById deleteRecipe, IUnitOfWork unitOfWork, IAzureStorageService storageService)
        {
            _getRecipe = getRecipe;
            _loggedUser = loggedUser;
            _deleteRecipe = deleteRecipe;
            _unitOfWork = unitOfWork;
            _storageService = storageService;
        }

        public async Task Execute(long id)
        {
            var user = await _loggedUser.getUser();
            var recipe = await _getRecipe.GetById(user, id);

            if (recipe is null)
                throw new GetRecipeException(ResourceExceptMessages.NO_RECIPE_FOUND);

            if (string.IsNullOrEmpty(recipe.ImageIdentifier) == false)
                await _storageService.Delete(user, recipe.ImageIdentifier);

            await _deleteRecipe.DeleteById(id);
            await _unitOfWork.Commit();
        }
    }
}
