using FluentValidation;
using ProjectAspNet.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.Validators.Recipe
{
    public class RecipeGenerateRequest : AbstractValidator<RequestGenerateRecipe>
    {
        public RecipeGenerateRequest()
        {
            RuleFor(d => d.Ingredients.Count).InclusiveBetween(1, 5);
            RuleFor(d => d.Ingredients).Must(d => d.Count == d.Distinct().Count());
            RuleFor(d => d.Ingredients).ForEach(d =>
            {
                d.Custom((value, context) =>
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        context.AddFailure(string.Empty);
                        return;
                    }

                    if(value.Count(c => c == ' ') > 3 || value.Count(c => c == '/') > 1)
                    {
                        context.AddFailure(string.Empty);
                        return;
                    }
                });
            });
        }
    }
}
