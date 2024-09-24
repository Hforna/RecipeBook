using CommonTestUtilities.DTOs;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.Recipe;
using FluentAssertions;
using ProjectAspNet.Application.UseCases.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public class RecipeGenerateTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestGenerateRecipeBuild.Build();
            var dto = new RecipeGenerateDtoTest().Build(request.Ingredients);

            var GenerateRecipeAi = new GenerateRecipeAiBuilder().Build(dto, request);
            var useCase = new GenerateRecipeUseCase(GenerateRecipeAi);

            var result = await useCase.Execute(request);
            result.Difficulty.Should().Be(ProjectAspNet.Communication.Requests.Enums.Difficulty.Easy);
        }
    }
}
