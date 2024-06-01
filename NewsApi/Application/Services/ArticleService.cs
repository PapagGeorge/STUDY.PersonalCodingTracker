using Application.Interfaces;
using System.Text.Json;
using Domain.Models;
using Application.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Services
{
    public class ArticleService : IArticleService
    {
        public const string BaseUrl = "https://newsapi.org/v2/top-headlines";
        public const string ApiKey = "5e666310837a4d05ab612db16657b36e";
        private readonly IArticleRepository _articleRepository;
        private readonly IDistributedCache _cache;
        private readonly HttpClient _httpClient; //IhttpClient todo
        

        public ArticleService(IArticleRepository articleRepository, IDistributedCache cache, HttpClient httpClient)
        {
            _articleRepository = articleRepository;
            _cache = cache;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", ApiKey);
            

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

            var apiUrl = $"https://newsapi.org/v2/top-headlines?country=gr&apiKey=5e666310837a4d05ab612db16657b36e"; //todo
            

            var response = await _httpClient.GetStringAsync(apiUrl);
            var articlesResponse = JsonSerializer.Deserialize<NewsResponse>(response, GetJsonSerializerOptions());

            if (articlesResponse != null && articlesResponse.Status == "ok" && articlesResponse.Articles.Any())
            {
                
                foreach (var article in articlesResponse.Articles)
                {
                    await CacheArticleAsync(article);
                }

                
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

        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
    }
}