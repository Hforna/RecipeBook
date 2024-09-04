using ProjectAspNet.Application;
using ProjectAspNet.Infrastructure;
using ProjectAspNet.Infrastructure.Extensions;
using ProjectAspNet.Infrastructure.Migrations;
using ProjectAspNet_API.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(opt => opt.Filters.Add(typeof(FilterExceptions)));

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);


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
