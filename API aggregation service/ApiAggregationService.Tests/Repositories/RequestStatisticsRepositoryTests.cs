using Domain.Models.RequestStatistics;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAggregationService.Tests.Repositories
{
    [TestFixture]
    public class RequestStatisticsRepositoryTests
    {
        [Test]
        public void AddRequestStatistic_ShouldAddToStatistics()
        {
            // Arrange
            var repository = new RequestStatisticsRepository();

            var statistic1 = new RequestStatistic
            {
                ApiName = "TestApi",
                ResponseTime = 50,
                Timestamp = DateTime.UtcNow
            };

            var statistic2 = new RequestStatistic
            {
                ApiName = "AnotherApi",
                ResponseTime = 150,
                Timestamp = DateTime.UtcNow
            };

            // Act
            repository.AddRequestStatistic(statistic1);
            repository.AddRequestStatistic(statistic2);

            var statistics = repository.GetAllStatistics().ToList();

            // Assert
            Assert.AreEqual(2, statistics.Count);

            var addedStatistic1 = statistics.FirstOrDefault(stat => stat.ApiName == "TestApi");
            var addedStatistic2 = statistics.FirstOrDefault(stat => stat.ApiName == "AnotherApi");

            Assert.IsNotNull(addedStatistic1);
            Assert.IsNotNull(addedStatistic2);

            Assert.AreEqual(statistic1.ResponseTime, addedStatistic1.ResponseTime);
            Assert.AreEqual(statistic2.ResponseTime, addedStatistic2.ResponseTime);
        }
    }
}
