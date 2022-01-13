using Domain.Commands.Handlers;
using Domain.Contracts.Commands;
using Domain.Contracts.Queries;
using Domain.Queries.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configurations
{
    public static partial class ServicesConfiguration
    {
        public static void ConfigureHandlers(this IServiceCollection services)
        {
            services.AddScoped<IProductCommandHandler, ProductCommandHandler>();
            services.AddScoped<IProductQueryHandler, ProductQueryHandler>();
        }    
    }
}