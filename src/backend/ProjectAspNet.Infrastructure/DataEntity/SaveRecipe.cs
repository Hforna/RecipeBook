using Microsoft.EntityFrameworkCore;
using ProjectAspNet.Domain.DTOs;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Entities.Recipes;
using ProjectAspNet.Domain.Enums;
using ProjectAspNet.Domain.Repositories.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Infrastructure.DataEntity
{
    public class SaveRecipe : ISaveRecipe, IFilterRecipe, IGetRecipeById
    {
        private readonly ProjectAspNetDbContext _dbContext;

        public SaveRecipe(ProjectAspNetDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(Recipe recipe)
        {
            await _dbContext.AddAsync(recipe);
        }

        public async Task<IList<Recipe>> FilterRecipe(UserEntitie user, FilterRecipeDto filter)
        {
            var query = _dbContext
                .Recipes
                .AsNoTracking()
                .Include(d => d.Ingredients)
                .Where(r => r.UserId == user.Id && r.Active);

            if (string.IsNullOrEmpty(filter.TitleIngredientsRecipe) == false)
            {
                query = query.Where(r => r.Title!.Contains(filter.TitleIngredientsRecipe) || r.Ingredients.Any(d => d.Item.Contains(filter.TitleIngredientsRecipe)));
            }
            if (filter.Difficulty.Any())
                query = query.Where(r => r.Difficulty.HasValue && filter.Difficulty.Contains(r.Difficulty.Value));

            if (filter.CookingTime.Any())
                query = query.Where(r => r.TimeRecipe.HasValue && filter.CookingTime.Contains(r.TimeRecipe.Value));

            if(filter.DishTypes.Any())
                query = query.Where(r => r.DishTypes.Any(d => filter.DishTypes.Contains(d.Type)));

            return await query.ToListAsync();
        }

        public async Task<Recipe?> GetById(UserEntitie user, long recipeId)
        {
            return await _dbContext
                .Recipes
                .AsNoTracking()
                .Include(r => r.Ingredients)
                .Include(r => r.Instructions)
                .Include(r => r.DishTypes)
                .FirstOrDefaultAsync(r => r.Active && (long)r.Id == recipeId && (long)r.UserId == (long)user.Id);
        }
    }
}
