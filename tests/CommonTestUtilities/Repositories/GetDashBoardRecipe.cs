using Moq;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Entities.Recipes;
using ProjectAspNet.Domain.Repositories.Recipe;
using ProjectAspNet.Domain.Repositories.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Repositories
{
    public class GetDashBoardRecipe
    {
        public IGetDashboardRecipe Build(UserEntitie user, IList<Recipe> recipes)
        {
            var mock = new Mock<IGetDashboardRecipe>();
            mock.Setup(d => d.GetDashboardRecipe(user)).ReturnsAsync(recipes);
            return mock.Object;
        }
    }
}
