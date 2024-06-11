using Application.Interfaces;
using Domain.DTO;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class NewsApiResponseRepository : INewsRepository
    {
        private readonly NewsDbContext _dbContext;

        public NewsApiResponseRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<NewsApiResponse> GetNewsAsync(string keyword)
        {
            var newsApiResponse = await (from a in _dbContext.NewsApiResponses
                                         join b in _dbContext.Articles
                                         on a.NewsApiResponseId equals b.NewsApiResponseId
                                         where EF.Functions.Like(b.Title, $"%{keyword}%") || EF.Functions.Like(b.Content, $"%{keyword}%")
                                         select new NewsApiResponse
                                         {
                                             NewsApiResponseId = a.NewsApiResponseId,
                                             Status = a.Status,
                                             TotalResults = a.TotalResults,
                                             Articles = a.Articles.Select(article => new Article
                                             {
                                                 ArticleId = article.ArticleId,
                                                 SourceId = article.SourceId,
                                                 SourceName = article.SourceName,
                                                 NewsApiResponseId = article.NewsApiResponseId,
                                                 Author = article.Author,
                                                 Title = article.Title,
                                                 Description = article.Description,
                                                 Url = article.Url,
                                                 UrlToImage = article.UrlToImage,
                                                 PublishedAt = article.PublishedAt,
                                                 Content = article.Content
                                             }).ToList()
                                         }).FirstOrDefaultAsync();

            return newsApiResponse;

        }

        public Task SetNewsAsync(NewsApiResponse newNews)
        {
            throw new NotImplementedException();
        }
    }
}
