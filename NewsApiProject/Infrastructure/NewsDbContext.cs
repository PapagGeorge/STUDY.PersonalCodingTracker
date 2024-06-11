using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure
{
    public class NewsDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<NewsApiResponse> NewsApiResponses { get; set; }
        public DbSet<Source> Sources { get; set; }

        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                 .HasOne(a => a.Source)
                 .WithMany(n => n.Articles)
                 .HasForeignKey(a => a.SourceId);

            modelBuilder.Entity<Article>()
                .HasOne(a => a.NewsApiResponse)
                .WithMany(n => n.Articles)
                .HasForeignKey(a => a.NewsApiResponseId);

            modelBuilder.Entity<Article>()
                .HasKey(a => a.ArticleId);
        }

    }
}
