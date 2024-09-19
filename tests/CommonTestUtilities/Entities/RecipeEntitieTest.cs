using Bogus;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Domain.Entities.Recipes;
using ProjectAspNet.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Entities
{
    public class RecipeEntitieTest
    {
        public static Recipe Build(long userId = 1)
        {
            var step = 1;
            var id = 1;
            return new Faker<Recipe>()
                .RuleFor(r => r.Id, () => 1)
                .RuleFor(r => r.Title, f => f.Lorem.Word())
                .RuleFor(r => r.Difficulty, f => f.PickRandom<Difficulty>())
                .RuleFor(r => r.TimeRecipe, f => f.PickRandom<CookingTime>())
                .RuleFor(r => r.DishTypes, f => f.Make(4, () => new DishTypeEntitie() { Id = id++, Type = f.PickRandom<ProjectAspNet.Domain.Enums.DishType>() }))
                .RuleFor(r => r.Ingredients, f => f.Make(6, () => new IngredientEntitie() { Id = id++, Item = f.Commerce.ProductName() }))
                .RuleFor(r => r.Instructions, f => f.Make(4, () => new InstructionsEntitie() { Id = id++, Step = step++, Text = f.Lorem.Paragraph() }))
                .RuleFor(r => r.UserId, f => userId);
        }
    }
}
