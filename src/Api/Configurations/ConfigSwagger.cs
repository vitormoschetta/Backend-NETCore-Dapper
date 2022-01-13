using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Api.Configurations
{
    public static partial class ServicesConfiguration
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Product Demo API",
                    Description = "A simple example ASP.NET Core Web API",                
                    Contact = new OpenApiContact
                    {
                        Name = "Vitor Moschetta",
                        Email = string.Empty,
                        Url = new Uri("https://vitormoschetta.github.io/")
                    }                   
                });
            });
        }   
    }
}