using Moq;
using ProjectAspNet.Domain.Repositories.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Repositories
{
    public class DeleteRecipeBuilder
    {
        public static IDeleteRecipeById Build()
        {
            var mock = new Mock<IDeleteRecipeById>();
            return mock.Object;
        }
    }
}
