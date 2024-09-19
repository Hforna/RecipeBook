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
    public class RequestFilterRecipeBuild
    {
        public static RequestFilterRecipe Build()
        {
            return new Faker<RequestFilterRecipe>()
                .RuleFor(r => r.TitleIngredientsRecipe, f => f.Commerce.ProductName())
                .RuleFor(r => r.CookingTime, f => f.Make(4, () => f.PickRandom<CookingTime>()))
                .RuleFor(r => r.Difficulty, f => f.Make(2, () => f.PickRandom<Difficulty>()))
                .RuleFor(r => r.DishTypes, f => f.Make(2, () => f.PickRandom<DishType>()));
        }
    }
}
