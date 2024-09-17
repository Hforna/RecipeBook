using Bogus;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Requests.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Request.Recipe
{
    public class RequestRecipeBuild
    {
        public static RequestRecipe Build(int length_text_instructions = 1, int make_instructions = 6)
        {
            var step = 1;
            return new Faker<RequestRecipe>()
                .RuleFor(r => r.Title, f => f.Lorem.Word())
                .RuleFor(r => r.Difficulty, f => f.PickRandom<Difficulty>())
                .RuleFor(r => r.TimeRecipe, f => f.PickRandom<CookingTime>())
                .RuleFor(r => r.DishType, f => f.Make(4, () => f.PickRandom<DishType>()))
                .RuleFor(r => r.Ingredients, f => f.Make(4, () => f.Commerce.ProductName()))
                .RuleFor(r => r.Instructions, f => f.Make(make_instructions, () => new RequestInstructions()
                {
                    Text = f.Lorem.Paragraph(length_text_instructions),
                    Step = step++,
                }));
        }
    }
}
