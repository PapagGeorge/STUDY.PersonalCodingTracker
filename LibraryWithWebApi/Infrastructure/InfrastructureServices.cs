using Application.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;


namespace Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection InfraServices (this IServiceCollection services)
        {
            services.AddScoped<IRentService, RentService>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();

            var dbConfiguration = ((DatabaseConfiguration)ConfigurationManager.GetSection("DatabaseConfigurationSection"));
            services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(dbConfiguration.ConnectionString));

            return services;

        }

    }
}
