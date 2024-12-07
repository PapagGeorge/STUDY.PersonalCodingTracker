using Microsoft.EntityFrameworkCore;
using Domain;

namespace Infrastructure;

public class OrderServiceDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Product> OrderItems { get; set; }
    
    public OrderServiceDbContext(DbContextOptions<OrderServiceDbContext> options) : base(options)
    {

    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure one-to-many relationship between Address and Order
        modelBuilder.Entity<Order>()
            .HasOne(o => o.ShippingAddress)
            .WithMany(a => a.Orders)
            .HasForeignKey(o => o.ShippingAddressId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure many-to-many relationship between Order and Product
        modelBuilder.Entity<OrderProduct>()
            .HasKey(op => op.Id);

        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderId);

        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Product)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.ProductId);
    }
}