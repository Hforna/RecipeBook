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

namespace ProjectAspNet.Infrastructure
{
    public static class DependencyInjectionInfrastructure
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            AddUserDbContext(services);
            AddDbContext(services);
        }

        public static void AddDbContext(IServiceCollection service)
        {
            var connection = "Server=DESKTOP-AIT91VG;Database=MydotnetProject;Trusted_Connection=True;TrustServerCertificate=True;";
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
