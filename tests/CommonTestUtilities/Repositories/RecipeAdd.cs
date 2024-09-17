using Moq;
using ProjectAspNet.Application.UseCases.Repositories.Recipe;
using ProjectAspNet.Domain.Repositories.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Repositories
{
    public class RecipeAdd
    {
        public static ISaveRecipe Build()
        {
            var mock = new Mock<ISaveRecipe>();
            return mock.Object;
        }
    }
}
