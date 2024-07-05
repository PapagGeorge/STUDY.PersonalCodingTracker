using Application.Interfaces;
using Domain.Models.RequestStatistics;
using System.Collections.Concurrent;

namespace Infrastructure.Repositories
{
    public class RequestStatisticsRepository : IRequestStatisticsRepository
    {
        private readonly ConcurrentBag<RequestStatistic> _statistics = new ConcurrentBag<RequestStatistic>();

        public void AddRequestStatistic(RequestStatistic statistic)
        {
            _statistics.Add(statistic);
        }

        public IEnumerable<RequestStatistic> GetAllStatistics()
        {
            return _statistics;
        }
    }
}
