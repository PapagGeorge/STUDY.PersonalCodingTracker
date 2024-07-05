using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;


namespace Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection InfraServices(this IServiceCollection services)
        {
            services.AddScoped<INewsApiClient, NewsApiClient>();
            services.AddScoped<IWeatherApiClient, WeatherApiClient>();
            services.AddScoped<IAstronomyPictureClient, AstronomyPictureClient>();
            services.AddSingleton<IRequestStatisticsRepository,  RequestStatisticsRepository>();
            services.AddHttpClient();

            return services;
        }
    }
}
