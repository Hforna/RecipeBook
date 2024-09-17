using CommonTestUtilities.Request.Recipe;
using FluentAssertions;
using ProjectAspNet.Application.Validators.Recipe;
using ProjectAspNet.Communication.Requests.Enums;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators
{
    public class ValidateRecipeTest
    {
        [Fact]
        public void Success()
        {
            var request = RequestRecipeBuild.Build();
            var validate = new RecipeValidator();
            var result = validate.Validate(request);
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("       ")]
        [InlineData(null)]
        [InlineData(" ")]
        public void Error_Title_Empty(string title)
        {
            var request = RequestRecipeBuild.Build();
            request.Title = title;

            var validate = new RecipeValidator();
            var result = validate.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(ResourceExceptMessages.TITLE_RECIPE_EMPTY);
        }

        [Fact]
        public void Error_Difficulty_Out_Enum()
        {
            var request = RequestRecipeBuild.Build();
            request.Difficulty = (Difficulty) 100;

            var validate = new RecipeValidator();
            var result = validate.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(ResourceExceptMessages.DIFFICULTY_OUT_ENUM);
        }

        [Fact]
        public void Error_TimeRecipe_Out_Enum()
        {
            var request = RequestRecipeBuild.Build();
            request.TimeRecipe = (CookingTime)200;

            var validate = new RecipeValidator();
            var result = validate.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(ResourceExceptMessages.TIME_RECIPE_OUT_ENUM);
        }
        
        [Fact]
        public void Error_TimeRecipe_Null()
        {
            var request = RequestRecipeBuild.Build();
            request.TimeRecipe = null;

            var validate = new RecipeValidator();
            var result = validate.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Success_Null_DishType()
        {
            var request = RequestRecipeBuild.Build();
            request.DishTypes = [];

            var validator = new RecipeValidator();
            var result = validator.Validate(request);
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Same_Steps()
        {
            var request = RequestRecipeBuild.Build();
            request.Instructions.First().Step = request.Instructions.Last().Step;

            var validator = new RecipeValidator();
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(ResourceExceptMessages.INSTRUCTION_STEPS_EQUALS);
        }

        [Fact]
        public void Error_Text_Instructions()
        {
            var request = RequestRecipeBuild.Build(2001, 1);

            var validator = new RecipeValidator();
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(ResourceExceptMessages.TEXT_INSTRUCTION_GREATER_2000);
        }

    }
}
