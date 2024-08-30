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

namespace ProjectAspNet.Infrastructure
{
    public static class DependencyInjectionInfrastructure
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddUserDbContext(services);
            AddDbContext(services, configuration);
        }

        public static void AddDbContext(IServiceCollection service, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("sqlserverconnection");
            service.AddDbContext<ProjectAspNetDbContext>(DbContextOptions => DbContextOptions.UseSqlServer(connection));
        }

        public static void AddUserDbContext(IServiceCollection service)
        {
            service.AddScoped<IUserEmailExists, UserRegisterDbContext>();
            service.AddScoped<IUserAdd, UserRegisterDbContext>();
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<IProductAdd, ProductRegisterDbContext>();
        }
    }
}
