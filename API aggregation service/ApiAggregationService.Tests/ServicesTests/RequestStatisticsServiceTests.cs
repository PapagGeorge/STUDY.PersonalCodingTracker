using Application.Interfaces;
using Application.Services;
using Domain.Models.RequestStatistics;
using Moq;

namespace ApiAggregationService.Tests.ServicesTests
{
    [TestFixture]
    public class RequestStatisticsServiceTests
    {
        [Test]
        public void GetRequestStatistics_ShouldReturnCorrectStatistics()
        {
            // Arrange
            var mockRepository = new Mock<IRequestStatisticsRepository>();

            var statistics = new List<RequestStatistic>
            {
                new RequestStatistic { ApiName = "Api1", ResponseTime = 50 },
                new RequestStatistic { ApiName = "Api1", ResponseTime = 150 },
                new RequestStatistic { ApiName = "Api2", ResponseTime = 180 },
                new RequestStatistic { ApiName = "Api2", ResponseTime = 220 },
                new RequestStatistic { ApiName = "Api3", ResponseTime = 80 },
                new RequestStatistic { ApiName = "Api3", ResponseTime = 110 },
                new RequestStatistic { ApiName = "Api3", ResponseTime = 250 }
            };

            mockRepository.Setup(repo => repo.GetAllStatistics()).Returns(statistics);

            var service = new RequestStatisticsService(mockRepository.Object);

            // Act
            var result = service.GetRequestStatistics();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count()); // We expect 3 different API groups

            // Check for each API group
            var api1Stats = result.FirstOrDefault(r => r.ApiName == "Api1");
            Assert.IsNotNull(api1Stats);
            Assert.AreEqual(2, api1Stats.TotalRequests);
            Assert.AreEqual(1, api1Stats.FastRequests);
            Assert.AreEqual(1, api1Stats.AverageRequests);
            Assert.AreEqual(0, api1Stats.SlowRequests);
            Assert.AreEqual(50, api1Stats.FastAverageTime);
            Assert.AreEqual(150, api1Stats.AverageAverageTime);
            Assert.AreEqual(0, api1Stats.SlowAverageTime);

            var api2Stats = result.FirstOrDefault(r => r.ApiName == "Api2");
            Assert.IsNotNull(api2Stats);
            Assert.AreEqual(2, api2Stats.TotalRequests);
            Assert.AreEqual(0, api2Stats.FastRequests);
            Assert.AreEqual(1, api2Stats.AverageRequests);
            Assert.AreEqual(1, api2Stats.SlowRequests);
            Assert.AreEqual(0, api2Stats.FastAverageTime);
            Assert.AreEqual(180, api2Stats.AverageAverageTime);
            Assert.AreEqual(220, api2Stats.SlowAverageTime);

            var api3Stats = result.FirstOrDefault(r => r.ApiName == "Api3");
            Assert.IsNotNull(api3Stats);
            Assert.AreEqual(3, api3Stats.TotalRequests);
            Assert.AreEqual(1, api3Stats.FastRequests);
            Assert.AreEqual(1, api3Stats.AverageRequests);
            Assert.AreEqual(1, api3Stats.SlowRequests);
            Assert.AreEqual(80, api3Stats.FastAverageTime);
            Assert.AreEqual(110, api3Stats.AverageAverageTime);
            Assert.AreEqual(250, api3Stats.SlowAverageTime);
        }
    }
}
