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
            try
            {
                var oneDayAgo = DateTime.UtcNow.AddDays(-2);
                List<Article> articles = await _dbContext.Articles
                .Where(a => a.PublishedAt >= oneDayAgo)
                .ToListAsync();

                return articles;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task InsertArticlesAsync(ICollection<Article> articles)
        {
            if(articles is null)
            {
                throw new ArgumentNullException(nameof(articles));
            }

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var article in articles)
                    {
                        var existingArticle = await _dbContext.Articles
                            .FirstOrDefaultAsync(a => a.Url == article.Url);

                        if (existingArticle == null)
                        {
                            await _dbContext.Articles.AddAsync(article);
                        }
                        else
                        {
                            existingArticle.Author = article.Author;
                            existingArticle.Title = article.Title;
                            existingArticle.Description = article.Description;
                            existingArticle.PublishedAt = article.PublishedAt;
                            existingArticle.UrlToImage = article.UrlToImage;
                            existingArticle.Content = article.Content;

                            _dbContext.Articles.Update(existingArticle);
                        }
                    }
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
