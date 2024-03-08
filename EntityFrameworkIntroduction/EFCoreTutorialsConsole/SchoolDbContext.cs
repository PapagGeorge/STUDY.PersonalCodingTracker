using Microsoft.EntityFrameworkCore;

namespace EFCoreTutorialsConsole
{
    public class SchoolDbContext : DbContext
    {
        public DbSet <Student> Students { get; set; }
        public DbSet <Grade> Grades { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SchoolDB;Trusted_Connection=True;");
        }
    }

    
}
