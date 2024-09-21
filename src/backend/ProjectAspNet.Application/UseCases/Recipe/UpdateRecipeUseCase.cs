using AutoMapper;
using ProjectAspNet.Application.UseCases.Repositories.Recipe;
using ProjectAspNet.Application.Validators.Recipe;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Domain.Entities.Recipes;
using ProjectAspNet.Domain.Repositories;
using ProjectAspNet.Domain.Repositories.Recipes;
using ProjectAspNet.Domain.Repositories.Users;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.Recipe
{
    public class UpdateRecipeUseCase : IUpdateRecipeUseCase
    {
        private readonly IUpdateRecipe _updateRecipe;
        private readonly ILoggedUser _userLogged;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRecipeUseCase(IUpdateRecipe updateRecipe, ILoggedUser userLogged, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _updateRecipe = updateRecipe;
            _userLogged = userLogged;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Execute(long id, RequestRecipe request)
        {
            Validate(request);
            var user = await _userLogged.getUser();
            var recipe = await _updateRecipe.GetById(user, id);

            if(recipe is null)
                throw new GetRecipeException(ResourceExceptMessages.NO_RECIPE_FOUND);

            recipe.Ingredients.Clear();
            recipe.Instructions.Clear();
            recipe.DishTypes.Clear();

            _mapper.Map(request, recipe);

            var instructions = request.Instructions.OrderBy(d => d.Step).ToList();
            for(int i = 0; i < instructions.Count; i++)
                instructions.ElementAt(i).Step = i + 1;

            recipe.Instructions = _mapper.Map<IList<InstructionsEntitie>>(instructions);

            _updateRecipe.Update(recipe!);
            await _unitOfWork.Commit();
        }

        public void Validate(RequestRecipe request)
        {
            var validate = new RecipeValidator();
            var result = validate.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new CreateRecipeException(errorMessages);
            }
        }
    }
}
