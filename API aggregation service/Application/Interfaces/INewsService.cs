using Domain.Models.NewsApiModels;

namespace Application.Interfaces
{
    public interface INewsService
    {
        Task<NewsApiResponse> GetNewsApiResponseAsync(string keyword, string sortBy, bool ascending = true);
    }
}
