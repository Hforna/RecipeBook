using Bogus;
using ProjectAspNet.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.DTOs
{
    public class InstructionsRecipeDtoTest
    {
        public InstructionsRecipeDto Build(int step = 1)
        {
            return new Faker<InstructionsRecipeDto>()
                .RuleFor(d => d.Step, () => step)
                .RuleFor(d => d.Text, (f) => f.Commerce.ProductName());
        }
    }
}
