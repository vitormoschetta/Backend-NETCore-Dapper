using System;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configurations
{
    public static partial class ServicesConfiguration
    {
        public static void ConfigureDbContext(this IServiceCollection services,
            IWebHostEnvironment environment, IConfiguration configuration)
        {
            services.AddTransient<IDbConnection>(db => new SqlConnection(
                configuration.GetConnectionString("DefaultConnection")));

            SqlMapper.AddTypeHandler(new MySqlGuidTypeHandler());
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));
        }

    }
}