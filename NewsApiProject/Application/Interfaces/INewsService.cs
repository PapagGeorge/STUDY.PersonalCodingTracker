using Domain.Models;

namespace Application.Interfaces
{
    public interface INewsService
    {
        Task<NewsApiResponse> GetNewsApiResponse(string keyword);
    }
}
