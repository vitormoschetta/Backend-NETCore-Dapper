using System;
using System.Data;
using Dapper;
using FluentMigrator.Runner;
using Infra.Migrations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configurations
{
    public static partial class ServicesConfiguration
    {
        private static IConfiguration Configuration;

        public static void ConfigureDbContext(this IServiceCollection services,
            IWebHostEnvironment environment, IConfiguration configuration)
        {
            Configuration = configuration;

            services.AddTransient<IDbConnection>(db => new SqlConnection(
                Configuration.GetConnectionString("DefaultConnection")));

            var serviceProvider = CreateServices();

            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }

            SqlMapper.AddTypeHandler(new MySqlGuidTypeHandler());
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));
        }

        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator        
                    .AddSqlServer()
                    // Set the connection string
                    .WithGlobalConnectionString(Configuration.GetConnectionString("DefaultConnection"))
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(AddLogTable).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }

    }
}