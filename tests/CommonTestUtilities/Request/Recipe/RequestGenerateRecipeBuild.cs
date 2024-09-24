using Bogus;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Request.Recipe
{
    public class RequestGenerateRecipeBuild
    {
        public static RequestGenerateRecipe Build(int arrayLength = 5)
        {
            return new Faker<RequestGenerateRecipe>()
                .RuleFor(r => r.Ingredients, (f) => f.Make(arrayLength, () => f.Commerce.ProductName()));
        }
    }
}
