using Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Domain.Models;
using Application.Extensions;

namespace Application.Services
{
    public class ArticleService : IArticleService
    {
        public const string baseUrl = "https://newsapi.org/v2/top-headlines?country=gr";
        public const string ApiKey = "5e666310837a4d05ab612db16657b36e";
        private readonly IArticleRepository _articleRepository;
        private readonly IDistributedCache _cache;
        private readonly HttpClient _httpClient;

        public ArticleService(IArticleRepository articleRepository, IDistributedCache cache, HttpClient httpClient)
        {
            _articleRepository = articleRepository;
            _cache = cache;
            _httpClient = httpClient;
        }

        public async Task<ICollection<Article>> GetLatestArticles()
        {
            var cachedArticles = await GetArticlesFromCacheAsync();
            if (cachedArticles != null && cachedArticles.Any())
            {
                return cachedArticles;
            }

            var dbArticles = await _articleRepository.GetLatestArticlesAsync();
            if (dbArticles != null && dbArticles.Any())
            {
                await CacheLatestArticlesAsync(dbArticles);

                foreach (var article in dbArticles)
                {
                    await CacheArticleAsync(article);
                }
                return dbArticles;
            }

            var apiArticles = await FetchAndCacheLatestArticlesFromApiAsync();
            if (apiArticles != null && apiArticles.Any())
            {
                await _articleRepository.InsertArticlesAsync(apiArticles);
                return apiArticles;
            }

            return new List<Article>();
        }

        private async Task CacheLatestArticlesAsync(ICollection<Article> articles)
        {
            var cacheKey = "latest_articles";
            await _cache.SetRecordAsync(cacheKey, articles);
        }

        private async Task<ICollection<Article>> GetArticlesFromCacheAsync()
        {
            
            string cacheKey = "latest_articles";
            var cachedArticles = await _cache.GetRecordAsync<List<Article>>(cacheKey, GetJsonSerializerOptions());
            return cachedArticles;
        }

        private async Task<ICollection<Article>> FetchAndCacheLatestArticlesFromApiAsync()
        {
            // Replace with your actual API URL
            var apiUrl = $"{baseUrl}&from={DateTime.UtcNow.AddDays(-1):yyyy-MM-dd}&to={DateTime.UtcNow:yyyy-MM-dd}&apiKey={ApiKey}";
            var response = await _httpClient.GetStringAsync(apiUrl);
            var articlesResponse = JsonSerializer.Deserialize<NewsResponse>(response, GetJsonSerializerOptions());

            if (articlesResponse != null && articlesResponse.Status == "ok" && articlesResponse.Articles.Any())
            {
                // Cache each article individually
                foreach (var article in articlesResponse.Articles)
                {
                    await CacheArticleAsync(article);
                }

                // Cache the entire collection of latest articles
                await _cache.SetRecordAsync("latest_articles", articlesResponse.Articles);
                return articlesResponse.Articles;
            }

            return new List<Article>();
        }

        private string GenerateCacheKey(Article article) => $"article_{article.Title}_{article.PublishedAt:yyyyMMddHHmmss}";

        public async Task CacheArticleAsync(Article article)
        {
            var cacheKey = GenerateCacheKey(article);
            await _cache.SetRecordAsync(cacheKey, article);
        }

        public async Task<Article?> GetArticleFromCacheAsync(Article article)
        {
            var cacheKey = GenerateCacheKey(article);
            return await _cache.GetRecordAsync<Article>(cacheKey, GetJsonSerializerOptions());
        }

        public static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions();
        }
    }
}

