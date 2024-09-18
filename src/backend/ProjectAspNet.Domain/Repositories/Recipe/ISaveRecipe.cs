using ProjectAspNet.Domain.Entities.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Repositories.Recipe
{
    public interface ISaveRecipe
    {
        public Task Add(Entities.Recipes.Recipe recipe);
    }
}
