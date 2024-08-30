using Microsoft.Extensions.DependencyInjection;
using ProjectAspNet.Application.Services.AutoMapper;
using ProjectAspNet.Application.Services.Cryptography;
using ProjectAspNet.Application.UseCases.Product;
using ProjectAspNet.Application.UseCases.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application
{
    public static class DependencyInjectionApplication
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddUserMapper(services);
            AddRegisterUserCase(services);
            AddCryptography(services);
            AddRegisterProductCase(services);
            AddProductMapper(services);
        }

        public static void AddUserMapper(IServiceCollection service)
        {
            var mapper = new AutoMapper.MapperConfiguration(x => { x.AddProfile(new UserMappper()); }).CreateMapper();
            service.AddScoped(opt => mapper);
        }

        public static void AddProductMapper(IServiceCollection service)
        {
            var mapper = new AutoMapper.MapperConfiguration(x => { x.AddProfile(new ProductMapper()); }).CreateMapper();
            service.AddScoped(opt => mapper);
        }

        public static void AddRegisterUserCase(IServiceCollection service)
        {
            service.AddScoped<IUserCase, RegisterUserCase>();
        }

        public static void AddRegisterProductCase(IServiceCollection service)
        {
            service.AddScoped<IProductCase, ProductUseCase>();
        }

        public static void AddCryptography(IServiceCollection service)
        {
            service.AddScoped(opt => new PasswordCryptography());
        }
    }
}
