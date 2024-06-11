using Domain.DTO;
using Domain.Models;

namespace Application.Interfaces
{
    public interface INewsRepository
    {
        Task<NewsApiResponse> GetNewsAsync(string keyword);
        Task SetNewsAsync(NewsApiResponse newNews);
    }
}
