using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repositories;


namespace Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection InfraServices(this IServiceCollection services)
        {
            services.AddScoped<INewsApiClient, NewsApiClient>();
            services.AddHttpClient();

            return services;
        }
    }
}
