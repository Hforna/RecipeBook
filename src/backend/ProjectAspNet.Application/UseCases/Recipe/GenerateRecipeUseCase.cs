using ProjectAspNet.Application.UseCases.Repositories.Recipe;
using ProjectAspNet.Application.Validators.Recipe;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Domain.Repositories.OpenAi;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectAspNet.Communication.Requests.Enums;

namespace ProjectAspNet.Application.UseCases.Recipe
{
    public class GenerateRecipeUseCase : IGenerateRecipeUseCase
    {
        private readonly IGenerateRecipeAi _generateRecipe;

        public GenerateRecipeUseCase(IGenerateRecipeAi generateRecipe) => _generateRecipe = generateRecipe;

        public async Task<ResponseGenerateRecipe> Execute(RequestGenerateRecipe request)
        {
            Validate(request);

            var response = await _generateRecipe.Generate(request.Ingredients);

            return new ResponseGenerateRecipe()
            {
                Title = response.Title,
                CookingTime = (CookingTime)response.CookingTime,
                Difficulty = Communication.Requests.Enums.Difficulty.Easy,
                Ingredients = response.Ingredients,
                Instructions = response.Instructions
            };
        }

        public void Validate(RequestGenerateRecipe request)
        {
            var validator = new RecipeGenerateRequest();
            var result = validator.Validate(request);

            if(result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new RecipeGenerateException(errorMessages);
            }
        }
    }
}
