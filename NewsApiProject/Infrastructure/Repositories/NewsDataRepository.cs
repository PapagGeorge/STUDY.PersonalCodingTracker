using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using Infrastructure.Constants;

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
            // Generate a unique ID for the combined NewsApiResponse based on the keyword
            var uniqueResponseId = GenerateUniqueResponseId(keyword);

            // Fetch articles that match the keyword
            var articles = await _dbContext.Articles
                .Where(a => a.Title.Contains(keyword) || a.Content.Contains(keyword) || a.Description.Contains(keyword))
                .Select(a => new Article
                {
                    ArticleId = a.ArticleId,
                    SourceId = a.SourceId,
                    SourceName = a.SourceName,
                    NewsApiResponseId = a.NewsApiResponseId,
                    Author = a.Author,
                    Title = a.Title,
                    Description = a.Description,
                    Url = a.Url,
                    UrlToImage = a.UrlToImage,
                    PublishedAt = a.PublishedAt,
                    Content = a.Content,
                })
                .ToListAsync();

            if (!articles.Any())
            {
                return null; // No matching articles found
            }

            // Construct the combined NewsApiResponse
            var newsApiResponse = new NewsApiResponse
            {
                NewsApiResponseId = uniqueResponseId,
                Status = "ok", // Assuming status ok if articles found
                TotalResults = articles.Count,
                Articles = articles
            };

            return newsApiResponse;
        }

        private int GenerateUniqueResponseId(string keyword)
        {
            // Generate a hash code from the keyword to use as a unique ID
            // Note: In a real-world application, you might want a more robust way of generating unique IDs
            return keyword.GetHashCode();
        }

        public async Task SetNewsAsync(NewsApiResponse newNews)
        {
            using (var connection = GetSqlConnection())
            {
                var transaction = connection.BeginTransaction();
                try
                {
                    int newsApiResponseId = await InsertNewsApiResponseAsync(connection, transaction, newNews);

                    foreach (var article in newNews.Articles)
                    {
                        if (!await ArticleExistsAsync(connection, transaction, article.Url))
                        {
                            await InsertArticlesAsync(connection, transaction, newNews, newsApiResponseId);
                        }
                    }

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
                if (!await ArticleExistsAsync(connection, transaction, article.Url))
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


        private async Task<bool> ArticleExistsAsync(SqlConnection connection, SqlTransaction transaction, string url)
        {
            using (var command = new SqlCommand(StoredProcedures.ArticleExists, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Url", url);
                int result = (int)command.ExecuteScalar();

                return result >= 1;
            }
        }

    }
}

          





