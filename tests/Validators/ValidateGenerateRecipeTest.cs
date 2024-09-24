using CommonTestUtilities.Request.Recipe;
using FluentAssertions;
using ProjectAspNet.Application.Validators.Recipe;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators
{
    public class ValidateGenerateRecipeTest
    {
        [Fact]
        public void Success()
        {
            var request = RequestGenerateRecipeBuild.Build();
            var validator = new RecipeGenerateRequest();
            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Array_Length()
        {
            var request = RequestGenerateRecipeBuild.Build(6);
            var validator = new RecipeGenerateRequest();
            var result = validator.Validate(request);

            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceExceptMessages.INGREDIENTS_GENERATE_GREATER);
        }
    }
}
