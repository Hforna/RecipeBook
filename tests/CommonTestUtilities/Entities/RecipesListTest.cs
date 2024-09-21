using Bogus;
using ProjectAspNet.Domain.Entities.Recipes;
using ProjectAspNet.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Entities
{
    public class RecipesListTest
    {
        public static IList<Recipe> Build(long userId = 1, uint count = 1)
        {
            if (count == 0)
                count = 1;

            var list = new List<Recipe>();

            for(int i = 0; i < count; i++)
            {
                var recipe = RecipeEntitieTest.Build(userId);
                list.Add(recipe);
            }

            return list;           
        }
    }
}
