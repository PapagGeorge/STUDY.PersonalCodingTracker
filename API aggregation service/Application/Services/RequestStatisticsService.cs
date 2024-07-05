using Application.Interfaces;
using Domain.Models.RequestStatistics;

namespace Application.Services
{
    public class RequestStatisticsService : IRequestStatisticsService
    {
        private readonly IRequestStatisticsRepository _requestStatisticsRepository;

        public RequestStatisticsService(IRequestStatisticsRepository requestStatisticsRepository)
        {
            _requestStatisticsRepository = requestStatisticsRepository;
        }
        public IEnumerable<ApiRequestStatistics> GetRequestStatistics()
        {
            var allStatistics = _requestStatisticsRepository.GetAllStatistics();

            var groupByApi = allStatistics.GroupBy(x => x.ApiName);

            var result = new List<ApiRequestStatistics>();

            foreach(var apiGroup in groupByApi)
            {
                var apiName = apiGroup.Key;
                var totalRequests = apiGroup.Count();

                var fastRequests = apiGroup.Where(stat => stat.ResponseTime < 100).ToList();
                var averageRequests = apiGroup.Where(stat => stat.ResponseTime >= 100 && stat.ResponseTime <= 200).ToList();
                var slowRequests = apiGroup.Where(stat => stat.ResponseTime > 200).ToList();

                var fastAverageTime = fastRequests.Any() ? fastRequests.Average(stat => stat.ResponseTime) : 0;
                var averageAverageTime = averageRequests.Any() ? averageRequests.Average(stat => stat.ResponseTime) : 0;
                var slowAverageTime = slowRequests.Any() ? slowRequests.Average(stat => stat.ResponseTime) : 0;

                result.Add(new ApiRequestStatistics
                {
                    ApiName = apiName,
                    TotalRequests = totalRequests,
                    FastRequests = fastRequests.Count(),
                    AverageRequests = averageRequests.Count(),
                    SlowRequests = slowRequests.Count(),
                    FastAverageTime = fastAverageTime,
                    AverageAverageTime = averageAverageTime,
                    SlowAverageTime = slowAverageTime
                });
            }
            return result;
        }
    }
}
