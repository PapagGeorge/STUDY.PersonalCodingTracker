using Domain.DTO;
using Domain.Models;

namespace Application.Interfaces
{
    public interface INewsApiResponseRepository
    {
        Task<NewsApiResponse> GetApiResponse(string keyword);
    }
}
