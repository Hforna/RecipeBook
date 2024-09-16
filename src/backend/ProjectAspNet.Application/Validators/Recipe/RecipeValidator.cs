using FluentValidation;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.Validators.Recipe
{
    public class RecipeValidator : AbstractValidator<RequestRecipe>
    {
        public RecipeValidator()
        {
            RuleFor(r => r.Title).NotEmpty().WithMessage(ResourceExceptMessages.TITLE_RECIPE_EMPTY);
            RuleFor(r => r.Difficulty).IsInEnum().WithMessage(ResourceExceptMessages.DIFFICULTY_OUT_ENUM);
            RuleFor(r => r.TimeRecipe).IsInEnum().WithMessage(ResourceExceptMessages.TIME_RECIPE_OUT_ENUM);            
        }
    }
}
