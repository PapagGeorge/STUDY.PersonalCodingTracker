using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Data.SqlClient;
using Infrastructure.Constants;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infrastructure.Repositories
{
    public class NewsDataRepository : BaseRepository, INewsDataRepository
    {
        private readonly NewsDbContext _dbContext;

        public NewsDataRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<NewsApiResponse> GetNewsAsync(string keyword)
        {
            var newsApiResponse = await (from a in _dbContext.NewsApiResponses
                                         join b in _dbContext.Articles
                                         on a.NewsApiResponseId equals b.NewsApiResponseId
                                         where b.Title.Contains(keyword) || b.Content.Contains(keyword)
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
                    int newsApiResponseId = await InsertNewsApiResponseAsync(connection, transaction, newNews);
                    await InsertArticlesAsync(connection, transaction, newNews, newsApiResponseId);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private async Task<int> InsertNewsApiResponseAsync(SqlConnection connection, SqlTransaction transaction, NewsApiResponse newNews)
        {
            using (var command = new SqlCommand(StoredProcedures.InsertNewsApiResponse, connection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Status", newNews.Status);
                command.Parameters.AddWithValue("@TotalResults", newNews.TotalResults);

                var newsApiResponseIdParam = new SqlParameter("@NewsApiResponseId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(newsApiResponseIdParam);

                await command.ExecuteNonQueryAsync();

                return (int)newsApiResponseIdParam.Value;
            }
        }

        private async Task InsertArticlesAsync(SqlConnection connection, SqlTransaction transaction, NewsApiResponse newNews, int newsApiResponseId)
        {
            foreach (var article in newNews.Articles)
            {
                // Insert source and get the Unique (SourceId)
                int sourceId = await InsertSourceAsync(connection, transaction, article.Source);

                // Insert article with SourceId and NewsApiResponseId
                using (var command = new SqlCommand(StoredProcedures.InsertArticle, connection, transaction))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Author", article.Author ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Title", article.Title ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", article.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Url", article.Url ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@UrlToImage", article.UrlToImage ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PublishedAt", article.PublishedAt ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Content", article.Content ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SourceId", sourceId);
                    command.Parameters.AddWithValue("@SourceName", article.Source.Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@NewsApiResponseId", newsApiResponseId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task<int> InsertSourceAsync(SqlConnection connection, SqlTransaction transaction, Source source)
        {
            using (var command = new SqlCommand(StoredProcedures.InsertSource, connection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SourceId", source.SourceId ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Name", source.Name);

                var uniqueParam = new SqlParameter("@Unique", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(uniqueParam);

                await command.ExecuteNonQueryAsync();

                return (int)uniqueParam.Value;
            }
        }

    }

}


