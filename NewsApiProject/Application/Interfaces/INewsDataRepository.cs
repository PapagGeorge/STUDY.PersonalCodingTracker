using Domain.DTO;
using Domain.Models;

namespace Application.Interfaces
{
    public interface INewsDataRepository
    {
        Task<NewsApiResponse> GetNewsAsync(string keyword);
        Task SetNewsAsync(NewsApiResponse newNews);
    }
}
