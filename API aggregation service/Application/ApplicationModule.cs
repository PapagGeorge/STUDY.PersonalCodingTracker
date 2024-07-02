using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Application.Services;

namespace Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IWeatherService, WeatherService>();

            return services;
        }
    }
}
