using Moq;
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
    public class GetRecipeByIdBuilder
    {
        private Mock<IGetRecipeById> _repository;

        public IGetRecipeById Build(UserEntitie user, Recipe recipe)
        {
            _repository = new Mock<IGetRecipeById>();
            _repository.Setup(d => d.GetById(user, recipe.Id)).ReturnsAsync(recipe);

            return _repository.Object;
        }
    }
}
