using Application.Interfaces;
using Application.Services;
using Domain.Models.NewsApiModels;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiAggregationService.Tests.ServicesTests
{
    [TestFixture]
    public class NewsServiceTests
    {
        private Mock<INewsApiClient> _mockNewsApiClient;
        private Mock<IDistributedCache> _mockDistributedCache;
        private NewsService _newsService;

        [SetUp]
        public void SetUp()
        {
            _mockNewsApiClient = new Mock<INewsApiClient>();
            _mockDistributedCache = new Mock<IDistributedCache>();
            _newsService = new NewsService(_mockNewsApiClient.Object, _mockDistributedCache.Object);
        }

        private static NewsApiResponse GetMockNewsApiResponse()
        {
            return new NewsApiResponse
            {
                Status = "ok",
                TotalResults = 2,
                Articles = new List<Article>
                {
                    new Article
                    {
                        Author = "Author1",
                        Title = "Title1",
                        Description = "Description1",
                        Url = "https://testurl1.com",
                        UrlToImage = "https://testimage1.com",
                        PublishedAt = new DateTime(2024, 1, 1),
                        Content = "Content1",
                        Source = new Source { SourceId = "1", Name = "Source1" }
                    },
                    new Article
                    {
                        Author = "Author2",
                        Title = "Title2",
                        Description = "Description2",
                        Url = "https://testurl2.com",
                        UrlToImage = "https://testimage2.com",
                        PublishedAt = new DateTime(2024, 1, 2),
                        Content = "Content2",
                        Source = new Source { SourceId = "2", Name = "Source2" }
                    }
                }
            };
        }

        private static byte[] SerializeToCacheFormat(NewsApiResponse newsApiResponse)
        {
            return JsonSerializer.SerializeToUtf8Bytes(newsApiResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        [Test]
        public async Task GetNewsApiResponseAsync_FetchesDataFromApi_WhenCacheIsEmpty()
        {
            // Arrange
            var mockNewsApiResponse = GetMockNewsApiResponse();
            var keyword = "testKeyword";
            _mockDistributedCache.Setup(x => x.GetAsync(It.IsAny<string>(), default))
                                 .ReturnsAsync((byte[])null);
            _mockNewsApiClient.Setup(x => x.GetNewsAsync(It.IsAny<string>()))
                              .ReturnsAsync(mockNewsApiResponse);

            // Act
            var result = await _newsService.GetNewsApiResponseAsync(keyword, "author", true);

            // Assert
            Assert.AreEqual(mockNewsApiResponse.TotalResults, result.TotalResults);
            _mockNewsApiClient.Verify(x => x.GetNewsAsync(It.IsAny<string>()), Times.Once);
            _mockDistributedCache.Verify(x => x.SetAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), default), Times.Once);
        }

        [Test]
        public async Task GetNewsApiResponseAsync_FetchesDataFromCache_WhenCacheIsNotEmpty()
        {
            // Arrange
            var mockNewsApiResponse = GetMockNewsApiResponse();
            var keyword = "testKeyword";
            var cacheData = SerializeToCacheFormat(mockNewsApiResponse);

            _mockDistributedCache.Setup(x => x.GetAsync(It.IsAny<string>(), default))
                                 .ReturnsAsync(cacheData);

            // Act
            var result = await _newsService.GetNewsApiResponseAsync(keyword, "author", true);

            // Assert
            Assert.AreEqual(mockNewsApiResponse.TotalResults, result.TotalResults);
            _mockNewsApiClient.Verify(x => x.GetNewsAsync(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task GetNewsApiResponseAsync_ReturnsDefaultResponse_WhenApiReturnsNoData()
        {
            // Arrange
            var keyword = "testKeyword";

            _mockDistributedCache.Setup(x => x.GetAsync(It.IsAny<string>(), default))
                                 .ReturnsAsync((byte[])null);
            _mockNewsApiClient.Setup(x => x.GetNewsAsync(It.IsAny<string>()))
                              .ReturnsAsync((NewsApiResponse)null);

            // Act
            var result = await _newsService.GetNewsApiResponseAsync(keyword, "author", true);

            // Assert
            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(0, result.TotalResults);
        }

        [Test]
        public async Task GetNewsApiResponseAsync_SortsDataByAuthorCorrectly()
        {
            // Arrange
            var mockNewsApiResponse = GetMockNewsApiResponse();
            var keyword = "testKeyword";

            _mockDistributedCache.Setup(x => x.GetAsync(It.IsAny<string>(), default))
                                 .ReturnsAsync((byte[])null);
            _mockNewsApiClient.Setup(x => x.GetNewsAsync(It.IsAny<string>()))
                              .ReturnsAsync(mockNewsApiResponse);

            // Act
            var result = await _newsService.GetNewsApiResponseAsync(keyword, "author", true);

            // Assert
            Assert.AreEqual("Author1", result.Articles.First().Author);
        }

        [Test]
        public async Task GetNewsApiResponseAsync_SortsDataByDateCorrectly()
        {
            // Arrange
            var mockNewsApiResponse = GetMockNewsApiResponse();
            var keyword = "testKeyword";

            _mockDistributedCache.Setup(x => x.GetAsync(It.IsAny<string>(), default))
                                 .ReturnsAsync((byte[])null);
            _mockNewsApiClient.Setup(x => x.GetNewsAsync(It.IsAny<string>()))
                              .ReturnsAsync(mockNewsApiResponse);

            // Act
            var result = await _newsService.GetNewsApiResponseAsync(keyword, "date", false);

            // Assert
            Assert.AreEqual(new DateTime(2024, 1, 2), result.Articles.First().PublishedAt);
        }
    }
}
