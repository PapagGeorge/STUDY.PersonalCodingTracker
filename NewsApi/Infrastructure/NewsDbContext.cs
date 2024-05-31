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
    }
}
