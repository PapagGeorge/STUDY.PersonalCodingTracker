using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Application.Interfaces;
using Application.Services;

namespace Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MsSql");

            services.AddDbContext<NewsDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<INewsApiResponseRepository, NewsApiResponseRepository>();
            services.AddScoped<INewsService, NewsService>();
            services.AddHttpClient<INewsApiClient, NewsApiClient>();
            return services;
            }
    }
}
