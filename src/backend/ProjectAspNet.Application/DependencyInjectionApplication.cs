using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectAspNet.Application.Services.AutoMapper;
using ProjectAspNet.Application.UseCases.Product;
using ProjectAspNet.Application.UseCases.Recipe;
using ProjectAspNet.Application.UseCases.Repositories.Recipe;
using ProjectAspNet.Application.UseCases.RepositoriesUseCases.Product;
using ProjectAspNet.Application.UseCases.RepositoriesUseCases.User;
using ProjectAspNet.Application.UseCases.User;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application
{
    public static class DependencyInjectionApplication
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddUserMapper(services, configuration);
            AddRegisterUserCase(services);
            AddRegisterProductCase(services);
        }

        public static void AddUserMapper(IServiceCollection service, IConfiguration configuration)
        {
            var sqIds = new SqidsEncoder<long>(new()
            {
                MinLength = configuration.GetValue<int>("settings:sqIds:MinLength"),
                Alphabet = configuration.GetValue<string>("settings:sqIds:Alphabet")!
            });

            var mapper = new AutoMapper.MapperConfiguration(x => { x.AddProfile(new UserMappper()); x.AddProfile(new ProductMapper()); x.AddProfile(new ProfileMapper(sqIds));}).CreateMapper();
            service.AddScoped(opt => mapper);
        }

        public static void AddRegisterUserCase(IServiceCollection service)
        {
            service.AddScoped<IUserCase, RegisterUserCase>();
            service.AddScoped<ILoginUser, LoginUserCase>();
            service.AddScoped<IGetProfileUseCase, GetUserProfileUseCase>();
            service.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            service.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
            service.AddScoped<ICreateRecipe, CreateRecipeUseCase>();
        }

        public static void AddRegisterProductCase(IServiceCollection service)
        {
            service.AddScoped<IProductCase, ProductUseCase>();
        }
    }
}
