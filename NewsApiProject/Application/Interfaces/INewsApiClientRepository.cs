using Domain.Models;

namespace Application.Interfaces
{
    public interface INewsApiClientRepository
    {
        Task<NewsApiResponse> GetNewsAsync(string keyword);
    }
}
