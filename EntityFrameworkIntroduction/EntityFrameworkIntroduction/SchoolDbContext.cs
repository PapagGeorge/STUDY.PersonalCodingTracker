using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkIntroduction
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("MyConnectionString");
        }

    }
}
