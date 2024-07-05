using Domain.Models.RequestStatistics;

namespace Application.Interfaces
{
    public interface IRequestStatisticsService
    {
        IEnumerable<ApiRequestStatistics> GetRequestStatistics();
    }
}
