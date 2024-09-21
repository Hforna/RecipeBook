using ProjectAspNet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Repositories.Recipes
{
    public interface IUpdateRecipe
    {
        public void Update(ProjectAspNet.Domain.Entities.Recipes.Recipe recipe);

        public Task<ProjectAspNet.Domain.Entities.Recipes.Recipe?> GetById(UserEntitie user, long id);
    }
}
