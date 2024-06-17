using Domain.Models;

namespace Application.Interfaces
{
    public interface INewsService
    {
        Task<NewsApiResponse> GetNewsApiResponse(string keyword, bool forceRefresh = false);
        Task SaveNewsApiResponse(NewsApiResponse newsApiResponse);
    }
}
