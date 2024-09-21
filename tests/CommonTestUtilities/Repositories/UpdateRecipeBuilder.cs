using Moq;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Entities.Recipes;
using ProjectAspNet.Domain.Repositories.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Repositories
{
    public class UpdateRecipeBuilder
    {
        public static IUpdateRecipe Build(UserEntitie user, Recipe recipe)
        {
            var mock = new Mock<IUpdateRecipe>(); 
            mock.Setup(d => d.GetById(user, recipe.Id)).ReturnsAsync(recipe);
            return mock.Object;
        }
    }
}
