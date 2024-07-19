using Domain.Models.NewsApiModels;
using Infrastructure.Repositories;
using Moq.Protected;
using Moq;
using Polly.CircuitBreaker;
using System.Net;
using System.Text.Json;
using Application.Interfaces;
using Domain.Models.AstronomyPictureModel;
using Microsoft.Extensions.Configuration;

namespace ApiAggregationService.Tests.HttpClientTests
{
    [TestFixture]
    public class AstronomyPictureClientTests
    {
        private Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private HttpClient _httpClient;
        private IAstronomyPictureClient _astronomyApiClient;
        private IRequestStatisticsRepository _requestStatisticsRepository;
        private IConfiguration _configuration;


        [SetUp]
        public void Setup()
        {
            // Mocking IConfiguration
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(config => config["ApiSettings:NasaApiBaseUrl"]).Returns("https://api.nasa.gov/planetary/apod?");
            configurationMock.Setup(config => config["ApiSettings:NasaApiKey"]).Returns("test_api_key");
            _configuration = configurationMock.Object;

            // Mocking IRequestStatisticsRepository
            var requestStatisticsRepositoryMock = new Mock<IRequestStatisticsRepository>();
            _requestStatisticsRepository = requestStatisticsRepositoryMock.Object;

            // Mocking HttpMessageHandler
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _httpMessageHandlerMock.Protected()
                .Setup("Dispose", ItExpr.IsAny<bool>())
                .Verifiable();

            // Initialize HttpClient
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);

            // Creating an instance of AstronomyPictureClient with mocks
            _astronomyApiClient = new AstronomyPictureClient(_httpClient, _requestStatisticsRepository, _configuration);
        }

        [Test]
        public async Task GetAstronomyPicturesAsync_ShouldReturnAstronomyApiResponse_WhenApiCallIsSuccessful()
        {
            //Arrange
            var expectedResponse = new AstronomyPicture
            {
                Date = DateTime.MinValue,
                Explanation = "testExplanation",
                HdUrl = "testHdUrl",
                MediaType = "testMediaType",
                ServiceVersion = "serviceVersionTest",
                Title = "testTitle",
                Url = "testUrl"
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

            //Act
            var result = await _astronomyApiClient.GetAstronomyPicturesAsync();
            
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResponse.Explanation, result.First().Explanation);
            Assert.AreEqual(expectedResponse.HdUrl, result.First().HdUrl);
            Assert.AreEqual(expectedResponse.Title, result.First().Title);
            Assert.AreEqual(expectedResponse.Date, result.First().Date);


        }

        [Test]
        public async Task GetAstronomyPicturesAsync_ShouldThrowException_WhenCircuitBreakerIsOpen()
        {
            //Arrange
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ThrowsAsync(new BrokenCircuitException());

            // Act
            var result = await _astronomyApiClient.GetAstronomyPicturesAsync();

            // Assert
            Assert.IsNull(result);

        }

        [Test]
        public async Task GetAstronomyPicturesAsync_ShouldThrowException_WhenApiCallFails()
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

            // Act
            var result = await _astronomyApiClient.GetAstronomyPicturesAsync();

            // Assert
            Assert.IsNull(result);
        }

        [TearDown]
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
