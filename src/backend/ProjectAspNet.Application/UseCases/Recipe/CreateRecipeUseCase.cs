using AutoMapper;
using ProjectAspNet.Application.UseCases.Repositories.Recipe;
using ProjectAspNet.Application.Validators.Recipe;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Domain.Entities.Recipes;
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
    public class CreateRecipeUseCase : ICreateRecipe
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaveRecipe _saveRecipe;
        private readonly ILoggedUser _loggedUser;

        public CreateRecipeUseCase(IMapper mapper, IUnitOfWork unitOfWork, ISaveRecipe saveRecipe, ILoggedUser loggedUser)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _saveRecipe = saveRecipe;
            _loggedUser = loggedUser;
        }
        public async Task<ResponseRecipe> Execute(RequestRecipe request)
        {
            var loggedUser = await _loggedUser.getUser();
            Validate(request);

            var recipe = _mapper.Map<RecipeEntitie>(request);
            recipe.UserId = loggedUser.Id;

            var instructions = recipe.Instructions.OrderBy(s => s.Step).ToList();
            for(int i = 0; i < instructions.Count; i++)
                instructions.ElementAt(i).Step = i + 1;

            recipe.Instructions = _mapper.Map<IList<InstructionsEntitie>>(instructions);

            await _saveRecipe.Add(recipe);
            await _unitOfWork.Commit();

            return _mapper.Map<ResponseRecipe>(recipe);
        }

        public void Validate(RequestRecipe request)
        {
            var validate = new RecipeValidator();
            var result = validate.Validate(request);

            if(result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new CreateRecipeException(errorMessages);
            }

        }
    }
}
