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
            RuleFor(r => r.Ingredients.Count).GreaterThan(0).WithMessage(ResourceExceptMessages.LIST_OUT_OF_RANGE);
            RuleFor(r => r.Instructions.Count).GreaterThan(0).WithMessage(ResourceExceptMessages.LIST_OUT_OF_RANGE);
            RuleForEach(r => r.DishType).IsInEnum().WithMessage(ResourceExceptMessages.DISH_TYPE_OUT_ENUM);
            RuleForEach(r => r.Ingredients).NotEmpty().WithMessage(ResourceExceptMessages.INGREDIENT_EMPTY);
            RuleForEach(r => r.Instructions).ChildRules(c =>
            {
                c.RuleFor(d => d.Step).GreaterThan(0).WithMessage(ResourceExceptMessages.STEP_LESS_1);
                c.RuleFor(d => d.Text)
                .MaximumLength(2000).WithMessage(ResourceExceptMessages.TEXT_INSTRUCTION_GREATER_2000)
                .NotEmpty().WithMessage(ResourceExceptMessages.TEXT_INSTRUCTION_EMPTY);
            });
            RuleFor(r => r.Instructions).Must(i => i.Select(d => d.Step).Distinct().Count() == i.Count).WithMessage(ResourceExceptMessages.INSTRUCTION_STEPS_EQUALS);
        }
    }
}
