using Application.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsApiResponseRepository _newsApiResponseRepository;
        private readonly INewsApiClient _newsApiClient;

        public NewsService(INewsApiResponseRepository newsApiResponseRepository, INewsApiClient newsApiClient)
        {
            _newsApiResponseRepository = newsApiResponseRepository;
            _newsApiClient = newsApiClient;
        }
        public async Task<NewsApiResponse> GetNewsApiResponse(string keyword)
        {
            var newsApiResponse = await _newsApiResponseRepository.GetApiResponseAsync(keyword);
            if (newsApiResponse != null)
            {
                return newsApiResponse;
            }

            newsApiResponse = await _newsApiClient.GetNewsAsync(keyword);
            if (newsApiResponse != null)
            {
                return newsApiResponse;
            }

            return default;
        }
    }
}
