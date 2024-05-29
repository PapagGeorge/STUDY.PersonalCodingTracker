using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<IpAddress> IpAddresses { get; set; }
    }
}
