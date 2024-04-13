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
            services.AddSingleton<IRentService, RentService>();
            services.AddSingleton<IBookRepository, BookRepository>();
            services.AddSingleton<IMemberRepository, MemberRepository>();

            var dbConfiguration = ((DatabaseConfiguration)ConfigurationManager.GetSection("DatabaseConfigurationSection"));
            services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(dbConfiguration.ConnectionString));

            return services;

        }

    }
}
