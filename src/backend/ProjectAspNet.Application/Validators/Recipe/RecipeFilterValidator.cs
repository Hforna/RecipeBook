using FluentValidation;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Requests.Enums;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.Validators.Recipe
{
    public class RecipeFilterValidator : AbstractValidator<RequestFilterRecipe>
    {
        public RecipeFilterValidator()
        {
            RuleForEach(r => r.DishTypes).IsInEnum().WithMessage(ResourceExceptMessages.DISH_TYPE_OUT_ENUM);
            RuleForEach(r => r.Difficulty).IsInEnum().WithMessage(ResourceExceptMessages.DIFFICULTY_OUT_ENUM);
            RuleForEach(r => r.CookingTime).IsInEnum().WithMessage(ResourceExceptMessages.TIME_RECIPE_OUT_ENUM);
        }
    }
}
