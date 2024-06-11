using Application.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsApiResponseRepository;
        private readonly INewsApiClientRepository _newsApiClient;

        public NewsService(INewsRepository newsApiResponseRepository, INewsApiClientRepository newsApiClient)
        {
            _newsApiResponseRepository = newsApiResponseRepository;
            _newsApiClient = newsApiClient;
        }
        public async Task<NewsApiResponse> GetNewsApiResponse(string keyword)
        {
            var newsApiResponse = await _newsApiResponseRepository.GetNewsAsync(keyword);
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
