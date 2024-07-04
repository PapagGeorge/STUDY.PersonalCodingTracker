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
            try
            {
                //Attempt to fetch cached response
                var newsApiClientResponse = await _distributedCache.GetRecordAsync<NewsApiResponse>(keyword, GetJsonSerializerOptions());

                if (newsApiClientResponse == null)
                {
                    //fetch from API if not in cache
                    newsApiClientResponse = await _newsApiClient.GetNewsAsync(keyword);

                    if(newsApiClientResponse == null)
                    {
                        newsApiClientResponse = CreateDefaultNewsApiResponse();
                    }
                    else
                    {
                        //Handle case where API or fallback mechanism didn't return valid data
                        await _distributedCache.SetRecordAsync(keyword, newsApiClientResponse);
                    }
                }

                //Sort Articles
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
            catch(Exception ex)
            {
                return CreateDefaultNewsApiResponse();
            }  
        }

        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        private NewsApiResponse CreateDefaultNewsApiResponse()
        {
            return new NewsApiResponse
            {
                Status = "error",
                TotalResults = 0,
                Articles = new List<Article>()
            };
        }
    }
}
