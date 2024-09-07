using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ProjectAspNet.Domain.Repositories;
using ProjectAspNet.Domain.Repositories.Users;
using ProjectAspNet.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectAspNet.Domain.Repositories.Products;
using Microsoft.Extensions.Configuration;
using FluentMigrator.Runner;
using System.Reflection;
using ProjectAspNet.Infrastructure.Extensions;

namespace ProjectAspNet.Infrastructure
{
    public static class DependencyInjectionInfrastructure
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositoriesDbContext(services);
            if (configuration.InMemoryEnviroment())
                return;
            AddDbContext(services, configuration);
            AddFluentMigratior(services, configuration);
        }

        public static void AddDbContext(IServiceCollection service, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("sqlserverconnection");
            service.AddDbContext<ProjectAspNetDbContext>(DbContextOptions => DbContextOptions.UseSqlServer(connection));
        }

        public static void AddRepositoriesDbContext(IServiceCollection service)
        {
            service.AddScoped<IUserEmailExists, UserRegisterDbContext>();
            service.AddScoped<IUserAdd, UserRegisterDbContext>();
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<IProductAdd, ProductRegisterDbContext>();
        }

        public static void AddFluentMigratior(IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("sqlserverconnection");
            service.AddFluentMigratorCore().ConfigureRunner(opt =>
            {
                opt.AddSqlServer().WithGlobalConnectionString(connectionString).ScanIn(Assembly.Load("ProjectAspNet.Infrastructure")).For.All();
            });
        }
    }
}
