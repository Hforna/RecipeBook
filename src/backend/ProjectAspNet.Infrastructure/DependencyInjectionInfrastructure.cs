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
using ProjectAspNet.Domain.Repositories.Security.Tokens;
using ProjectAspNet.Infrastructure.Security.Tokens;
using ProjectAspNet.Domain.Repositories.Security;
using ProjectAspNet.Domain.Repositories.Recipe;
using ProjectAspNet.Domain.Repositories.Recipes;
using OpenAI_API;
using ProjectAspNet.Domain.Repositories.OpenAi;
using ProjectAspNet.Infrastructure.OpenAi;
using ProjectAspNet.Domain.Repositories.Storage;
using ProjectAspNet.Infrastructure.Storage;
using Azure.Messaging.ServiceBus;
using ProjectAspNet.Domain.Repositories.ServiceBus;
using ProjectAspNet.Infrastructure.ServiceBus;
using ProjectAspNet.Infrastructure.Security.Cryptography;

namespace ProjectAspNet.Infrastructure
{
    public static class DependencyInjectionInfrastructure
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositoriesDbContext(services);
            AddJwtToken(services, configuration);
            AddUserLogged(services);
            AddCryptography(services, configuration);
            AddOpenAi(services, configuration);
            AddAzureStorage(services, configuration);
            AddServiceBus(services, configuration);
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

        public static void AddJwtToken(IServiceCollection services, IConfiguration configuration)
        {
            var expirateTime = configuration.GetValue<uint>("Token:Expiratetime");
            var signKey = configuration.GetValue<string>("Token:Signkey");
            services.AddScoped<ITokenGenerator>(opt => new GenerateToken(expirateTime, signKey!));
            services.AddScoped<ITokenValidator>(opt => new ValidateToken(signKey!));
        }

        public static void AddUserLogged(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();

        public static void AddRepositoriesDbContext(IServiceCollection service)
        {
            service.AddScoped<IUserEmailExists, UserRegisterDbContext>();
            service.AddScoped<IUserAdd, UserRegisterDbContext>();
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<IProductAdd, ProductRegisterDbContext>();
            service.AddScoped<IUserIdentifierExists, UserRegisterDbContext>();
            service.AddScoped<IGetUserUpdate, UserRegisterDbContext>();
            service.AddScoped<IGetUserTracking, UserRegisterDbContext>();
            service.AddScoped<ISaveRecipe, SaveRecipe>();
            service.AddScoped<IFilterRecipe, SaveRecipe>();
            service.AddScoped<IGetRecipeById, SaveRecipe>();
            service.AddScoped<IDeleteRecipeById, SaveRecipe>();
            service.AddScoped<IUpdateRecipe, SaveRecipe>();
            service.AddScoped<IGetDashboardRecipe, SaveRecipe>();
            service.AddScoped<IDeleteUser, UserRegisterDbContext>();
            service.AddScoped<IUserByEmail, UserRegisterDbContext>();
            service.AddScoped<IRefreshTokenRepository, RefreshTokenDbContext>();
        }

        public static void AddOpenAi(IServiceCollection services, IConfiguration configuration)
        {
            var openAiApi = configuration.GetValue<string>("settings:OpenAi:ApiKey");
            var api = new APIAuthentication(openAiApi);
            services.AddScoped<IOpenAIAPI>(d => new OpenAIAPI(api));
            services.AddScoped<IGenerateRecipeAi, GenerateRecipeService>();
        }

        public static void AddFluentMigratior(IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("sqlserverconnection");
            service.AddFluentMigratorCore().ConfigureRunner(opt =>
            {
                opt.AddSqlServer().WithGlobalConnectionString(connectionString).ScanIn(Assembly.Load("ProjectAspNet.Infrastructure")).For.All();
            });

            service.AddScoped<FluentMigrator.Runner.Processors.ProcessorOptions>();
        }

        public static void AddCryptography(IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<ICryptography, BCryptService>();
        }

        private static void AddAzureStorage(IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("settings:blobStorage:azure");
            if(string.IsNullOrEmpty(connectionString) == false)
                service.AddScoped<IAzureStorageService>(d => new AzureStorageService(new Azure.Storage.Blobs.BlobServiceClient(connectionString)));
        }

        private static void AddServiceBus(IServiceCollection service, IConfiguration configuration)
        {
            var clientConnection = configuration.GetValue<string>("settings:serviceBus:azure");

            var client = new ServiceBusClient(clientConnection, new ServiceBusClientOptions
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            });

            var sender = new DeleteUserSender(client.CreateSender("user"));
            service.AddScoped<IDeleteUserSender>(opt => sender);

            var processor = new DeleteUserProcessor(client.CreateProcessor("user", new ServiceBusProcessorOptions()
            {
                MaxConcurrentCalls = 1,
            }));

            service.AddSingleton(processor);
        }
    }
}
