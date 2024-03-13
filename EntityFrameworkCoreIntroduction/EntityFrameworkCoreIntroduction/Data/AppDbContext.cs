using EntityFrameworkCoreIntroduction.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreIntroduction.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet <Employee> Employees { get; set; }
        public DbSet <Manager> Managers { get; set; }
        public string ConnectionString { get; }

        public AppDbContext()
        {
            ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=CompanyManagement;Integrated Security=true";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
