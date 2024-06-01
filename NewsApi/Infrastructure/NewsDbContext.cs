using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure
{
    public class NewsDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }

        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasOne(a => a.Source)
                .WithMany()
                .HasForeignKey(a => a.SourceId);

            modelBuilder.Entity<Source>()
                .Property(s => s.Id)
                .IsRequired(false);

            
            modelBuilder.Entity<Article>().ToTable("Articles");
            modelBuilder.Entity<Source>().ToTable("Sources");
        }
    }
}
