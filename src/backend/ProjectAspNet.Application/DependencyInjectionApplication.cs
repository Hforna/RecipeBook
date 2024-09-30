using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectAspNet.Application.Services.AutoMapper;
using ProjectAspNet.Application.UseCases.Login;
using ProjectAspNet.Application.UseCases.Product;
using ProjectAspNet.Application.UseCases.Recipe;
using ProjectAspNet.Application.UseCases.Repositories.Login;
using ProjectAspNet.Application.UseCases.Repositories.Recipe;
using ProjectAspNet.Application.UseCases.Repositories.User;
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
            AddSqIds(services, configuration);
            AddUserMapper(services);
            AddRegisterUserCase(services);
            AddRegisterProductCase(services);
        }

        public static void AddUserMapper(IServiceCollection service)
        {
            service.AddScoped(opt =>
                new AutoMapper.MapperConfiguration(x => {
                    var sqIds = opt.GetService<SqidsEncoder<long>>()!;
                    x.AddProfile(new UserMappper()); x.AddProfile(new ProductMapper()); x.AddProfile(new ProfileMapper(sqIds));
                }).CreateMapper()
            );
        }

        public static void AddSqIds(IServiceCollection services, IConfiguration configuration)
        {
            var sqids = new SqidsEncoder<long>(new()
            {
                Alphabet = configuration.GetValue<string>("settings:sqIds:Alphabet")!,
                MinLength = configuration.GetValue<int>("settings:sqIds:MinLegth")
            });

            services.AddSingleton(sqids);
        }

        public static void AddRegisterUserCase(IServiceCollection service)
        {
            service.AddScoped<IUserCase, RegisterUserCase>();
            service.AddScoped<ILoginUser, LoginUserCase>();
            service.AddScoped<IGetProfileUseCase, GetUserProfileUseCase>();
            service.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            service.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
            service.AddScoped<ICreateRecipe, CreateRecipeUseCase>();
            service.AddScoped<IFilterRecipeUseCase, FilterRecipeUseCase>();
            service.AddScoped<IGetRecipeUseCase, GetRecipeUseCase>();
            service.AddScoped<IDeleteRecipe, DeleteRecipeUseCase>();
            service.AddScoped<IUpdateRecipeUseCase, UpdateRecipeUseCase>();
            service.AddScoped<IRecipeDashboardUseCase, RecipeDashboardUseCase>();
            service.AddScoped<IGenerateRecipeUseCase, GenerateRecipeUseCase>();
            service.AddScoped<IUpdateImageRecipe,  UpdateImageRecipeUseCase>();
            service.AddScoped<IRequesteDeleteUserUseCase, RequestDeleteUserUseCase>();
            service.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
            service.AddScoped<IGoogleLoginUseCase, GoogleLoginUseCase>();
        }

        public static void AddRegisterProductCase(IServiceCollection service)
        {
            service.AddScoped<IProductCase, ProductUseCase>();
        }
    }
}
