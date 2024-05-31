using Domain.Models;

namespace Application.Interfaces
{
    public interface IArticleService
    {
        Task<ICollection<Article>> GetLatestArticles();
    }
}
