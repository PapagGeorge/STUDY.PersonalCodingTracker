using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Domain
{
    public class TravelAgencyDbContext : DbContext
    {
        public DbSet<Accommodation> Accommodation { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageAccommodation> Package_Accomodation { get; set; }
        public DbSet<PackageDestination> Package_Destination { get; set; }
        public DbSet<PackageService> Package_Service { get; set; }
        public DbSet<PackageTransportation> Package_Transportation { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Transportation> Transportation { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source=DESKTOP;Database=TravelAgencyManagementSystem;Integrated Security=SSPI;Trust Server Certificate=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PackageAccommodation>()
                .HasKey(pa => new { pa.PackageId, pa.AccommodationId });

            modelBuilder.Entity<PackageDestination>()
                .HasKey(pa => new { pa.PackageId, pa.DestinationId });

            modelBuilder.Entity<PackageService>()
                .HasKey(pa => new { pa.PackageId, pa.ServiceId });

            modelBuilder.Entity<PackageTransportation>()
                .HasKey(pa => new { pa.PackageId, pa.TransportationId });



            base.OnModelCreating(modelBuilder);
        }
    }
}
