using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure.NewsDbContext
{
    public class NewsDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<NewsApiResponse> NewsApiResponses { get; set; }

        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewsApiResponse>()
                .HasKey(n => n.NewsApiResponseId);

            modelBuilder.Entity<NewsApiResponse>()
                .HasMany(n => n.Articles)
                .WithOne(a => a.NewsApiResponse)
                .HasForeignKey(a => a.NewsApiResponseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Article>()
                .HasKey(a => a.ArticleId);

            modelBuilder.Entity<Article>()
                .OwnsOne(a => a.Source);
        }


    }
}
