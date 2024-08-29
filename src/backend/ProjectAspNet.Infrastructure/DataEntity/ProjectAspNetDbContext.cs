using Microsoft.EntityFrameworkCore;
using ProjectAspNet.Domain.Entities;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectAspNetDbContext).Assembly);
        }

    }
}
