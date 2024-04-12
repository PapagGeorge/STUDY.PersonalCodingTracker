using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System.Configuration;

namespace Infrastructure
{
    public class SalesDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }




        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbConfiguration = ((DatabaseConfigurationSection)ConfigurationManager.GetSection("DataBaseConfigurationSection"));
            var connectionString = dbConfiguration.ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);
        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(prop => prop.ColorId)
                .IsRequired(false);

            modelBuilder.Entity<Order>()
                .Property(o => o.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            




        }
    }
}
