using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Entities.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Repositories.Recipe
{
    public interface IGetRecipeById
    {
        public Task<ProjectAspNet.Domain.Entities.Recipes.Recipe?> GetById(UserEntitie user, long recipeId);
    }
}
