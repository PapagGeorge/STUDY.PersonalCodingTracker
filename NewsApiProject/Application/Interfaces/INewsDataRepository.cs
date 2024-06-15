using Domain.DTO;
using Domain.Models;
using System.Data.SqlClient;

namespace Application.Interfaces
{
    public interface INewsDataRepository
    {
        Task<NewsApiResponse> GetNewsAsync(string keyword);
        Task SetNewsAsync(NewsApiResponse newNews);
    }
}
