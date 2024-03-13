using EntityFrameworkCoreIntroduction.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreIntroduction.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet <Employee> Employees { get; set; }
        public DbSet <Manager> Managers { get; set; }
        public DbSet <EmployeeDetails> EmployeeDetails { get; set; }
        public DbSet <Project> Projects { get; set; }
        public DbSet <EmployeeProject> EmployeeProjects { get; set; }
        public string ConnectionString { get; }

        public AppDbContext()
        {
            ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=CompanyManagement;Integrated Security=true";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeProject>()
                .HasKey(ep => new {ep.EmployeeId, ep.ProjectId});

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Employee)
                .WithMany(emp => emp.EmployeeProjects)
                .HasForeignKey(ep => ep.EmployeeId);

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Project)
                .WithMany(proj => proj.EmployeeProjects)
                .HasForeignKey(ep => ep.ProjectId);
        }
    }
}
