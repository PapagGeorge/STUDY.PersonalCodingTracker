using Autofac;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;


namespace Infrastructure
{
    public class InfrastructureModule : Module
    {
        private readonly IConfiguration _configuration;
        public InfrastructureModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            var connectionString = _configuration.GetConnectionString("MsSql");

            builder.Register<NewsDbContext>(context =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<NewsDbContext>();
                optionsBuilder.UseSqlServer(connectionString);
                return new NewsDbContext(optionsBuilder.Options);
            });

            builder.RegisterType<ArticleRepository>().As<IArticleRepository>();
        }
    }
}
