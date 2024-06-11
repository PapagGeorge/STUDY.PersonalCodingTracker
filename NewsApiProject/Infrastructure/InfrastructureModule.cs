using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MsSql");

            services.AddDbContext<NewsDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<INewsService, NewsService>();
            services.AddHttpClient<INewsApiClientRepository, NewsApiClientRepository>();
            return services;
            }
    }
}
