using Dapper;
using FluentMigrator;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Infrastructure.Migrations
{
    public static class DatabaseMigration
    {

        public static void Migrate(string connectionString, IServiceProvider serviceProvider)
        {
            EnsureDatabaseSqlServer(connectionString);
            MigrationDatabase(serviceProvider);
        }
        private static void EnsureDatabaseSqlServer(string connectionString)
        {
            var stringBuilder = new SqlConnectionStringBuilder(connectionString);
            var dbName = stringBuilder.InitialCatalog;
            stringBuilder.Remove(dbName);
            var connectServer = new SqlConnection(stringBuilder.ConnectionString);
            var parameters = new DynamicParameters();
            parameters.Add("name", dbName);

            var dbInServer = connectServer.Query("SELECT * FROM sys.databases where name = @name", parameters);
            if (dbInServer is null)
                connectServer.Execute($"CREATE DATABASE {dbName}");
            
        }

        private static void MigrationDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.ListMigrations();

            runner.MigrateUp();
        }
    }
}
