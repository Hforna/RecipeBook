using Bogus;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Domain.DTOs;
using ProjectAspNet.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.DTOs
{
    public class RecipeGenerateDtoTest
    {
        public GenerateRecipeDto Build(IList<string> ingredients, int instructions = 3)
        {
            var instrucstions = new List<InstructionsRecipeDto>();
            var step = 1;

            for (int i = 0; i < 3; i++)
            {
                instrucstions.Add(new InstructionsRecipeDtoTest().Build(step++));
            }

            return new Faker<GenerateRecipeDto>()
                .RuleFor(g => g.Ingredients, () => ingredients)
                .RuleFor(g => g.CookingTime, (f) => f.PickRandom<CookingTime>())
                .RuleFor(g => g.Title, f => f.Commerce.ProductName())
                .RuleFor(g => g.Instructions, instrucstions);
        }
    }
}
