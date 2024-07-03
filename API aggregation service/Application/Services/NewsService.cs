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
        public async Task<NewsApiResponse> GetNewsApiResponseAsync(string keyword, string sortBy, bool ascending = true)
        {
            var newsApiClientResponse = await _newsApiClient.GetNewsAsync(keyword);

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
    }
}
