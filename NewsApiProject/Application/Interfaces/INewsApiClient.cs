using Domain.Models;

namespace Application.Interfaces
{
    public interface INewsApiClient
    {
        Task<NewsApiResponse> GetNewsAsync(string keyword);
    }
}
