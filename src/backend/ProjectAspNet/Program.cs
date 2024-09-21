using ProjectAspNet.Application;
using ProjectAspNet.Infrastructure;
using ProjectAspNet.Infrastructure.Extensions;
using ProjectAspNet.Infrastructure.Migrations;
using ProjectAspNet_API.Filters;
using ProjectAspNet.Converters;
using Microsoft.OpenApi.Models;
using ProjectAspNet.Domain.Repositories.Security.Tokens;
using ProjectAspNet.Token;
using ProjectAspNet.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new StringConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<FilterBindId>();

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                        Enter 'Bearer' [space] and then your token in the text input below.
                        Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddMvc(opt => opt.Filters.Add(typeof(FilterExceptions)));

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<ITokenReceptor, TokenRecepetor>();

builder.Services.AddRouting(opt => opt.LowercaseUrls = true);
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var dd = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

if (builder.Configuration.InMemoryEnviroment() == false)
    DatabaseMigration.Migrate(builder.Configuration.GetConnectionString("sqlserverconnection")!, dd.ServiceProvider);

app.Run();

public partial class Program
{

}
