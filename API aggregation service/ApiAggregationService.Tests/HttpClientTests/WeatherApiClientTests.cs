using Application.Interfaces;
using Infrastructure.Repositories;
using Moq.Protected;
using Moq;
using Polly.CircuitBreaker;
using System.Net;
using System.Text.Json;
using Domain.Models.WeatherBitApi;

namespace ApiAggregationService.Tests.HttpClientTests
{
    [TestFixture]
    public class WeatherApiClientTests
    {
        private Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private HttpClient _httpClient;
        private IWeatherApiClient _weatherApiClient;
        private IRequestStatisticsRepository requestStatisticsRepository;

        [SetUp]
        public void Setup()
        {
            var requestStatisticsRepositoryMock = new Mock<IRequestStatisticsRepository>();
            requestStatisticsRepository = requestStatisticsRepositoryMock.Object;
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _weatherApiClient = new WeatherApiClient(_httpClient, requestStatisticsRepository);
        }

        [Test]
        public async Task GetWeatherAsync_ShouldReturnWeatherData_WhenApiCallIsSuccessful()
        {
            // Arrange
            var expectedResponse = new WeatherData
            {
                Count = 1,
                DataList = new List<Data>
        {
            new Data
            {
                WindCdir = "N",
                Rh = 50.0f,
                Pod = "d",
                Lon = -122.4194,
                Pres = 1013.0,
                Timezone = "PST",
                ObTime = "2023-07-05 12:00",
                CountryCode = "US",
                Clouds = 20.0f,
                Vis = 10.0f,
                WindSpd = 5.0,
                Gust = 7.0,
                WindCdirFull = "North",
                AppTemp = 24.0,
                StateCode = "CA",
                Ts = 1657036800,
                HAngle = 0.0f,
                Dewpt = 10.0,
                Weather = new Weather
                {
                    Icon = "c01d",
                    Code = 800,
                    Description = "Clear sky"
                },
                Uv = 5.0f,
                Aqi = 50.0f,
                Station = "KSFO",
                Sources = new List<string> { "KSFO", "E1234" },
                WindDir = 0.0f,
                ElevAngle = 45.0,
                Datetime = "2023-07-05:12",
                Precip = 0.0,
                Ghi = 1000.0,
                Dni = 800.0,
                Dhi = 500.0,
                SolarRad = 200.0,
                CityName = "San Francisco",
                Sunrise = "06:00",
                Sunset = "20:00",
                Temp = 25.0,
                Lat = 37.7749,
                Slp = 1013.0
            }
        }
            };

            var jsonResponse = JsonSerializer.Serialize(expectedResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonResponse)
                });

            // Act
            var result = await _weatherApiClient.GetWeatherAsync("US", "San Francisco");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResponse.Count, result.Count);
            Assert.AreEqual(expectedResponse.DataList.Count, result.DataList.Count);
            Assert.AreEqual(expectedResponse.DataList[0].CityName, result.DataList[0].CityName);
            Assert.AreEqual(expectedResponse.DataList[0].Weather.Description, result.DataList[0].Weather.Description);
        }
        [Test]
        public async Task GetWeatherAsync_ShouldThrowException_WhenCircuitBreakerIsOpen()
        {
            // Arrange
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ThrowsAsync(new BrokenCircuitException());

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _weatherApiClient.GetWeatherAsync("TC", "Test City"));
            Assert.AreEqual("Circuit breaker is open. Unable to fetch news from the API", ex.Message);
        }

        [Test]
        public void GetWeatherAsync_ShouldThrowException_WhenApiCallFails()
        {
            // Arrange
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError
                });

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _weatherApiClient.GetWeatherAsync("TC", "Test City"));
            Assert.IsTrue(ex.Message.StartsWith("An error occured while fetching news from the API"));
        }

        [TearDown]
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
