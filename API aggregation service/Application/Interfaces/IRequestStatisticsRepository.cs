using Domain.Models.RequestStatistics;

namespace Application.Interfaces
{
    public interface IRequestStatisticsRepository
    {
        void AddRequestStatistic(RequestStatistic statistic);
        IEnumerable<RequestStatistic> GetAllStatistics();
    }
}
