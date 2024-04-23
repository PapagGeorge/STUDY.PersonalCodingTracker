using Application.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection InfraServices(this IServiceCollection services)
        {
            var dbConfiguration = ((DatabaseConfiguration)ConfigurationManager.GetSection("DatabaseConfigurationSection"));
            services.AddDbContext<BetDbContext>(options => options.UseSqlServer(dbConfiguration.ConnectionString));

            services.AddScoped<IBetRepository, BetRepository>();
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<IResultRepository, ResultRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketBetRepository, TicketBetRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;

        }
    }
}
