using Application.Interfaces;
using Domain.Models.NewsApiModels;

namespace Application.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsApiClient _newsApiClient;

        public NewsService(INewsApiClient newsApiClient)
        {
            _newsApiClient = newsApiClient;
        }
        public async Task<NewsApiResponse> GetNewsApiResponseAsync(string keyword)
        {
            var newsApiClientResponse = await _newsApiClient.GetNewsAsync(keyword);
            return newsApiClientResponse;
        }
    }
}
