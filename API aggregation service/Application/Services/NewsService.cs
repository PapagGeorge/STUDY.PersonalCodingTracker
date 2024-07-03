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
        public async Task<NewsApiResponse> GetNewsApiResponseAsync(string keyword, string sortBy = "date", bool ascending = true)
        {
            var newsApiClientResponse = await _newsApiClient.GetNewsAsync(keyword);

            if (newsApiClientResponse.Articles != null)
            {
                if (sortBy == "date")
                {
                    newsApiClientResponse.Articles = ascending
                        ? newsApiClientResponse.Articles.OrderBy(a => a.PublishedAt).ToList()
                        : newsApiClientResponse.Articles.OrderByDescending(a => a.PublishedAt).ToList();
                }
            }
            return newsApiClientResponse;
        }
    }
}
