using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
                });
        }
    }
}
