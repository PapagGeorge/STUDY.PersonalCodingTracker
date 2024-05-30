using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Infrastructure.DatabaseContext;
using Infrastructure.Repositories;
using Application.Interfaces;
using Infrastructure.UnitOfWorkStructure;

namespace Infrastructure
{
    public class InfrastructureModule : Module
    {
        private readonly IConfiguration _configuration;

        public InfrastructureModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public InfrastructureModule()
        {
            
        }

        protected override void Load(ContainerBuilder builder)
        {
            var connectionString = _configuration.GetConnectionString("MsSql");

            builder.Register<ApplicationDbContext>(context =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(connectionString);

                return new ApplicationDbContext(optionsBuilder.Options);
            }).AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<AssignmentRepository>().As<IAssignmentRepository>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
        }
    }
}
