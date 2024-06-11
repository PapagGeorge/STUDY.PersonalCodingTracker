using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Data.SqlClient;
using Infrastructure.Constants;

namespace Infrastructure.Repositories
{
    public class NewsRepository : BaseRepository, INewsRepository
    {
        private readonly NewsDbContext _dbContext;

        public NewsRepository(NewsDbContext dbContext)
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

        public async Task SetNewsAsync(NewsApiResponse newNews)
        {
            using (var connection = GetSqlConnection())
            {
                var transaction = connection.BeginTransaction();
                try
                {
                    await SetNewsApiResponseAsync(connection, transaction, newNews);
                    await SetArticlesAsync(connection, transaction, newNews);
                    await SetSourceAsync(connection, transaction, newNews);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task SetArticlesAsync(SqlConnection connection, SqlTransaction transaction, NewsApiResponse newNews)
        {
            using (var command = new SqlCommand(StoredProcedures.InsertArticle, connection, transaction))
            {
                var articles = newNews.Articles.ToList();

                foreach (var article in articles)
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Author", article.Author);
                    command.Parameters.AddWithValue("@Title", article.Title);
                    command.Parameters.AddWithValue("@Description", article.Description);
                    command.Parameters.AddWithValue("@Url", article.Url);
                    command.Parameters.AddWithValue("@UrlToImage", article.UrlToImage);
                    command.Parameters.AddWithValue("@PublishedAt", article.PublishedAt);
                    command.Parameters.AddWithValue("@Content", article.Content);
                    command.Parameters.AddWithValue("@SourceId", article.SourceId);
                    command.Parameters.AddWithValue("@SourceName", article.SourceName);
                    command.Parameters.AddWithValue("@NewsApiResponseId", article.NewsApiResponseId);

                    command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task SetNewsApiResponseAsync(SqlConnection connection, SqlTransaction transaction, NewsApiResponse newNews)
        {
            using (var command = new SqlCommand(StoredProcedures.InsertNewsApiResponse, connection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Status", newNews.Status);
                command.Parameters.AddWithValue("@TotalResults", newNews.TotalResults);

                command.ExecuteNonQueryAsync();
            }
        }

        public async Task SetSourceAsync(SqlConnection connection, SqlTransaction transaction, NewsApiResponse newNews)
        {
            using (var command = new SqlCommand(StoredProcedures.InsertSource, connection, transaction))
            {
                var articles = newNews.Articles.ToList();

                foreach (var article in articles)
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SourceId", article.SourceId);
                    command.Parameters.AddWithValue("@Name", article.SourceName);

                    command.ExecuteNonQueryAsync();
                }
            }
        }
    }

}


