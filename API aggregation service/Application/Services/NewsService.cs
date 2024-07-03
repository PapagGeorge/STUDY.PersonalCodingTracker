using Application.Interfaces;
using Domain.Models.NewsApiModels;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Application.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsApiClient _newsApiClient;
        private readonly IDistributedCache _distributedCache;

        public NewsService(INewsApiClient newsApiClient, IDistributedCache distributedCache)
        {
            _newsApiClient = newsApiClient;
            _distributedCache = distributedCache;
        }
        public async Task<NewsApiResponse> GetNewsApiResponseAsync(string keyword, string sortBy, bool ascending = true)
        {
            var newsApiClientResponse = await _distributedCache.GetRecordAsync<NewsApiResponse>(keyword, GetJsonSerializerOptions());

            if(newsApiClientResponse == null)
            {
                newsApiClientResponse = await _newsApiClient.GetNewsAsync(keyword);
                await _distributedCache.SetRecordAsync(keyword, newsApiClientResponse);
            }

            if (newsApiClientResponse.Articles != null)
            {
                if (sortBy == "author")
                {
                    newsApiClientResponse.Articles = ascending
                        ? newsApiClientResponse.Articles.OrderBy(a => a.Author).ThenBy(x => x.PublishedAt).ToList()
                        : newsApiClientResponse.Articles.OrderBy(a => a.Author).ThenByDescending(x => x.PublishedAt).ToList();

                }

                if (sortBy == "date")
                {
                    newsApiClientResponse.Articles = ascending
                        ? newsApiClientResponse.Articles.OrderBy(a => a.PublishedAt).ToList()
                        : newsApiClientResponse.Articles.OrderByDescending(a => a.PublishedAt).ToList();

                }
            }
            return newsApiClientResponse;
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
