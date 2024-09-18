using Microsoft.EntityFrameworkCore;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Entities.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Infrastructure.DataEntity
{
    public class ProjectAspNetDbContext : DbContext
    {
        public ProjectAspNetDbContext(DbContextOptions options) : base(options) { }

        public DbSet<UserEntitie> Users { get; set; }
        public DbSet<ProductEntitie> Products { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<DishTypeEntitie> DishType { get; set; }
        public DbSet<IngredientEntitie> Ingredients { get; set; }
        public DbSet<InstructionsEntitie> Instructions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectAspNetDbContext).Assembly);
        }

    }
}
