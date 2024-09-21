using ProjectAspNet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Repositories.Recipes
{
    public interface IGetDashboardRecipe
    {
        public Task<IList<ProjectAspNet.Domain.Entities.Recipes.Recipe>> GetDashboardRecipe(UserEntitie user);
    }
}
