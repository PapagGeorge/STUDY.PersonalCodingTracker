using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreIntroduction.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
