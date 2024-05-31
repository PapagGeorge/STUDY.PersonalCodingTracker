using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        public NewsDbContext _dbContext;
        public ArticleRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ICollection<Article>> GetLatestArticlesAsync()
        {
            List<Article> articles = await _dbContext.Articles
                .Where(article => article.PublishedAt > DateTime.UtcNow.Subtract(TimeSpan.FromHours(24))).ToListAsync();
            return articles;
        }

        
    }
}
