using CommonTestUtilities.Entities;
using Moq;
using ProjectAspNet.Domain.DTOs;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Entities.Recipes;
using ProjectAspNet.Domain.Repositories.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Repositories
{
    public class RecipeFilterBUilder
    {
        private Mock<IFilterRecipe>? _filterRecipe;

        public IFilterRecipe Build(UserEntitie user, FilterRecipeDto filter, IList<Recipe> recipe)
        {
            var mock = new Mock<IFilterRecipe>();
            _filterRecipe = mock;
            _filterRecipe.Setup(f => f.FilterRecipe(user, filter)).ReturnsAsync(recipe);
            return mock.Object;
        }
    }
}
