using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System.Configuration;

namespace Infrastructure
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base (options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbConfiguration = ((DatabaseConfiguration)ConfigurationManager.GetSection("DatabaseConfigurationSection"));
            optionsBuilder.UseSqlServer(dbConfiguration.ConnectionString);
        }

    }
}
