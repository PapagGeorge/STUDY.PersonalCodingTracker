using Microsoft.EntityFrameworkCore;
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
            services.AddHostedService<NewsUpdateService>();

            return services;
        }
    }
}
