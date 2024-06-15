using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Application.Services
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache _distributedCache;

        public NewsService(IUnitOfWork unitOfWork,
            IDistributedCache distributedCache)
        {
            _unitOfWork = unitOfWork;
            _distributedCache = distributedCache;
        }
        public async Task<NewsApiResponse> GetNewsApiResponse(string keyword)
        {
            var cacheResponse = await _distributedCache.GetRecordAsync<NewsApiResponse>(keyword, GetJsonSerializerOptions());

            if (cacheResponse != null)
            {
                return cacheResponse;
            }

            var newsApiResponse = await _unitOfWork.NewsDataRepository.GetNewsAsync(keyword);
            if (newsApiResponse != null)
            {
                await _distributedCache.SetRecordAsync(keyword, newsApiResponse);
                return newsApiResponse;
            }

            newsApiResponse = await _unitOfWork.NewsApiClientRepository.GetNewsAsync(keyword);

            if (newsApiResponse != null)
            {
                await _unitOfWork.NewsDataRepository.SetNewsAsync(newsApiResponse);
                
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
