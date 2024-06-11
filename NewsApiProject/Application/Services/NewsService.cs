using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Application.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsApiResponseRepository;
        private readonly INewsApiClientRepository _newsApiClient;
        private readonly IDistributedCache _distributedCache;

        public NewsService(INewsRepository newsApiResponseRepository,
            INewsApiClientRepository newsApiClient,
            IDistributedCache distributedCache)
        {
            _newsApiResponseRepository = newsApiResponseRepository;
            _newsApiClient = newsApiClient;
            _distributedCache = distributedCache;
        }
        public async Task<NewsApiResponse> GetNewsApiResponse(string keyword)
        {
            var cacheResponse = await _distributedCache.GetRecordAsync<NewsApiResponse>(keyword, GetJsonSerializerOptions());

            if (cacheResponse != null)
            {
                return cacheResponse;
            }

            var newsApiResponse = await _newsApiResponseRepository.GetNewsAsync(keyword);
            if (newsApiResponse != null)
            {
                await _distributedCache.SetRecordAsync(keyword, newsApiResponse);
                return newsApiResponse;
            }

            newsApiResponse = await _newsApiClient.GetNewsAsync(keyword);

            if (newsApiResponse != null)
            {
                await _newsApiResponseRepository.SetNewsAsync(newsApiResponse);
                await _distributedCache.SetRecordAsync(keyword, newsApiResponse);
            }

            return newsApiResponse; 
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
