using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Entities.Recipes;
using ProjectAspNet.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTest
{
    public class DataBaseInMemoryApi : WebApplicationFactory<Program>
    {
        private UserEntitie _user = default!;
        private string _password = string.Empty;
        private Recipe? _recipe;

        public string getEmail() => _user.Email;
        public string getPassword() => _password;
        public string getUsername() => _user.Name;
        public Guid getUserIdentifier() => _user.UserIdentifier;
        public long getRecipeId() => _recipe!.Id;
        public string getRecipeName() => _recipe!.Title!;
        public IList<DishTypeEntitie> getDishType() => _recipe!.DishTypes;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(services =>
                {
                    var verifyDbContext = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ProjectAspNetDbContext>));

                    if (verifyDbContext is not null)
                        services.Remove(verifyDbContext);

                    var serviceProvider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                    services.AddDbContext<ProjectAspNetDbContext>(opt =>
                    {
                        opt.UseInMemoryDatabase("InMemoryDbForTesting");
                        opt.UseInternalServiceProvider(serviceProvider);
                    });
                    using var scope = services.BuildServiceProvider().CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<ProjectAspNetDbContext>();
                    dbContext.Database.EnsureDeleted();
                    (_user, _password) = UserEntitieTest.Build();
                    _recipe = RecipeEntitieTest.Build();
                    _recipe.Id = _user.Id;
                    dbContext.Recipes.Add(_recipe);
                    dbContext.Users.Add(_user);
                    dbContext.SaveChanges();
                });
        }
    }
}
