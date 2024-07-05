using Application.Interfaces;
using Application.Services;
using Domain.Models.AstronomyPictureModel;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System.Text.Json;

namespace ApiAggregationService.Tests.ServicesTests
{
    [TestFixture]
    public class AstronomyPictureServiceTests
    {
        private Mock<IAstronomyPictureClient> _mockAstronomyPictureClient;
        private Mock<IDistributedCache> _mockDistributedCache;
        private AstronomyPictureService _astronomyPictureService;

        [SetUp]
        public void SetUp()
        {
            _mockAstronomyPictureClient = new Mock<IAstronomyPictureClient>();
            _mockDistributedCache = new Mock<IDistributedCache>();
            _astronomyPictureService = new AstronomyPictureService(_mockAstronomyPictureClient.Object, _mockDistributedCache.Object);
        }

        private static IEnumerable<AstronomyPicture> GetMockAstronomyPictures()
        {
            return new List<AstronomyPicture>
            {
                new AstronomyPicture
                {
                    Date = new DateTime(2024, 1, 1),
                    Explanation = "Test Explanation 1",
                    HdUrl = "https://testurl1.com",
                    MediaType = "image",
                    ServiceVersion = "v1",
                    Title = "Test Title 1",
                    Url = "https://testurl1.com"
                },
                new AstronomyPicture
                {
                    Date = new DateTime(2024, 1, 2),
                    Explanation = "Test Explanation 2",
                    HdUrl = "https://testurl2.com",
                    MediaType = "image",
                    ServiceVersion = "v1",
                    Title = "Test Title 2",
                    Url = "https://testurl2.com"
                }
            };
        }

        private static byte[] SerializeToCacheFormat(IEnumerable<AstronomyPicture> astronomyPictures)
        {
            return JsonSerializer.SerializeToUtf8Bytes(astronomyPictures, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        [Test]
        public async Task GetAstronomyPictures_FetchesDataFromApi_WhenCacheIsEmpty()
        {
            // Arrange
            var mockAstronomyPictures = GetMockAstronomyPictures();
            var cacheKey = "sort_date_asc_True";

            _mockDistributedCache.Setup(x => x.GetAsync(It.IsAny<string>(), default))
                                 .ReturnsAsync((byte[])null);
            _mockAstronomyPictureClient.Setup(x => x.GetAstronomyPicturesAsync(It.IsAny<string>(), It.IsAny<string>()))
                                       .ReturnsAsync(mockAstronomyPictures);

            // Act
            var result = await _astronomyPictureService.GetAstronomyPictures(sortBy: "date", ascending: true);

            // Assert
            Assert.AreEqual(mockAstronomyPictures.Count(), result.Count());
            _mockAstronomyPictureClient.Verify(x => x.GetAstronomyPicturesAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mockDistributedCache.Verify(x => x.SetAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), default), Times.Once);
        }

        [Test]
        public async Task GetAstronomyPictures_FetchesDataFromCache_WhenCacheIsNotEmpty()
        {
            // Arrange
            var mockAstronomyPictures = GetMockAstronomyPictures();
            var cacheKey = "sort_date_asc_True";
            var cacheData = SerializeToCacheFormat(mockAstronomyPictures);

            _mockDistributedCache.Setup(x => x.GetAsync(It.IsAny<string>(), default))
                                 .ReturnsAsync(cacheData);

            // Act
            var result = await _astronomyPictureService.GetAstronomyPictures(sortBy: "date", ascending: true);

            // Assert
            Assert.AreEqual(mockAstronomyPictures.Count(), result.Count());
            _mockAstronomyPictureClient.Verify(x => x.GetAstronomyPicturesAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task GetAstronomyPictures_ReturnsDefaultResponse_WhenApiReturnsNoData()
        {
            // Arrange
            var cacheKey = "sort_date_asc_True";

            _mockDistributedCache.Setup(x => x.GetAsync(It.IsAny<string>(), default))
                                 .ReturnsAsync((byte[])null);
            _mockAstronomyPictureClient.Setup(x => x.GetAstronomyPicturesAsync(It.IsAny<string>(), It.IsAny<string>()))
                                       .ReturnsAsync((IEnumerable<AstronomyPicture>)null);

            // Act
            var result = await _astronomyPictureService.GetAstronomyPictures(sortBy: "date", ascending: true);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Error: No data available", result.First().Explanation);
        }

        [Test]
        public async Task GetAstronomyPictures_SortsDataCorrectly()
        {
            // Arrange
            var mockAstronomyPictures = GetMockAstronomyPictures();
            var cacheKey = "sort_date_asc_True";

            _mockDistributedCache.Setup(x => x.GetAsync(It.IsAny<string>(), default))
                                 .ReturnsAsync((byte[])null);
            _mockAstronomyPictureClient.Setup(x => x.GetAstronomyPicturesAsync(It.IsAny<string>(), It.IsAny<string>()))
                                       .ReturnsAsync(mockAstronomyPictures);

            // Act
            var result = await _astronomyPictureService.GetAstronomyPictures(sortBy: "date", ascending: false);

            // Assert
            Assert.AreEqual(mockAstronomyPictures.OrderByDescending(ap => ap.Date).First().Date, result.First().Date);
        }
    }
}
