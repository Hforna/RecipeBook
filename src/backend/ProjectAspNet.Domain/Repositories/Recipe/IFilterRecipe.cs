using ProjectAspNet.Domain.DTOs;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Entities.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Repositories.Recipe
{
    public interface IFilterRecipe
    {
        public Task<IList<Entities.Recipes.Recipe>> FilterRecipe(UserEntitie user, FilterRecipeDto filter);
    }
}
