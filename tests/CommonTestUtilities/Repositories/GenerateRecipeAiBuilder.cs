using Moq;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Domain.DTOs;
using ProjectAspNet.Domain.Repositories.OpenAi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Repositories
{
    public class GenerateRecipeAiBuilder
    {
        public IGenerateRecipeAi Build(GenerateRecipeDto dto, RequestGenerateRecipe request)
        {
            var mock = new Mock<IGenerateRecipeAi>();
            mock.Setup(g => g.Generate(request.Ingredients)).ReturnsAsync(dto);

            return mock.Object;
        }
    }
}
