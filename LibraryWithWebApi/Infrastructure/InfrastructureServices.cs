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
            services.AddDbContext<LibraryDbContext>(options
                => options.UseSqlServer("Data Source=DESKTOP;Database=NewLibrary;Integrated Security=SSPI;Trust Server Certificate=True;"));
            services.AddSingleton<IRentService, RentService>();
            services.AddSingleton<IBookRepository, BookRepository>();
            services.AddSingleton<IMemberRepository, MemberRepository>();

            //var dbConfiguration = ((DatabaseConfiguration)ConfigurationManager.GetSection("DatabaseConfigurationSection"));
            

            return services;

        }

    }
}
