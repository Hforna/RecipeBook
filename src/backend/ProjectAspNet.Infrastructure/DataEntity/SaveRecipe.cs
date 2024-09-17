using ProjectAspNet.Domain.Entities.Recipes;
using ProjectAspNet.Domain.Repositories.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Infrastructure.DataEntity
{
    public class SaveRecipe : ISaveRecipe
    {
        private readonly ProjectAspNetDbContext _dbContext;

        public SaveRecipe(ProjectAspNetDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(RecipeEntitie recipe)
        {
            await _dbContext.AddAsync(recipe);
        }
    }
}
