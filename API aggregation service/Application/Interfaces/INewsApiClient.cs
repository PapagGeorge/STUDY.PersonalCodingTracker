using Domain.Models.NewsApiModels;

namespace Application.Interfaces
{
    public interface INewsApiClient
    {
        Task<NewsApiResponse> GetNewsAsync(string keyword);
    }
}
