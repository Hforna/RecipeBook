using Dapper;
using Microsoft.Data.SqlClient;
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

        public static void Migrate(string connectionString)
        {
            EnsureDatabaseSqlServer(connectionString);
        }
        private static void EnsureDatabaseSqlServer(string connectionString)
        {
            var serverStringBuilder = new SqlConnectionStringBuilder(connectionString);
            var sbName = serverStringBuilder.InitialCatalog;
            serverStringBuilder.Remove("Database");

            using var connectDatabase = new SqlConnection(serverStringBuilder.ConnectionString);

            var parameters = new DynamicParameters();
            parameters.Add("name", sbName);

            var records = connectDatabase.Query("SELECT * FROM sys.databases WHERE name = @name", parameters);

            if(records.Any() == false)
            {
                connectDatabase.Execute($"CREATE DATABASE {sbName}");
            }
        }
    }
}
