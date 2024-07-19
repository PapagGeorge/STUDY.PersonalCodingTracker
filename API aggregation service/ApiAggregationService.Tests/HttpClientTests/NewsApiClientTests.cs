using Moq;
using Application.Interfaces;
using Infrastructure.Repositories;
using Domain.Models.NewsApiModels;
using Moq.Protected;
using System.Net;
using System.Text.Json;
using Polly;
using Polly.CircuitBreaker;
using Microsoft.Extensions.Configuration;

namespace ApiAggregationService.Tests.HttpClientTests
{
    [TestFixture]
    public class NewsApiClientTests
    {
        private Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private HttpClient _httpClient;
        private INewsApiClient _newsApiClient;
        private IRequestStatisticsRepository _requestStatisticsRepository;
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            // Mocking IConfiguration
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(config => config["ApiSettings:NewsApiBaseUrl"]).Returns("https://newsapi.org/v2/everything");
            configurationMock.Setup(config => config["ApiSettings:NewsApiKey"]).Returns("test_api_key");
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

            // Creating an instance of NewsApiClient with mocks
            _newsApiClient = new NewsApiClient(_httpClient, _requestStatisticsRepository, _configuration);
        }

        [Test]
        public async Task GetNewsAsync_ShouldReturnNewsApiResponse_WhenApiCallIsSuccessful()
        {
            //Arrange
            var expectedResponse = new NewsApiResponse
            {
                Status = "ok",
                TotalResults = 1,
                Articles = new List<Article>
                {
                    new Article
                    {
                        Author = "Test Author",
                        Title = "Test Article",
                        Description = "Test Description",
                        Url = "http://test.com",
                        UrlToImage = "Test UrlToImage",
                        PublishedAt = DateTime.UtcNow,
                        Content = "Test Content",
                        Source = new Source()
                        {
                            SourceId = "Test SourceId",
                            Name = "Test Name"
                        }
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

            //Act
            var result = await _newsApiClient.GetNewsAsync("test");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResponse.Status, result.Status);
            Assert.AreEqual(expectedResponse.TotalResults, result.TotalResults);
            Assert.AreEqual(expectedResponse.Articles.Count, result.Articles.Count);
            Assert.AreEqual(expectedResponse.Articles[0].Title, result.Articles[0].Title);


        }

        [Test]
        public async Task GetNewsAsync_ShouldThrowException_WhenCircuitBreakerIsOpen()
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
            var result = await _newsApiClient.GetNewsAsync("test");

            // Assert
            Assert.IsNull(result);

        }

        [Test]
        public async Task GetNewsAsync_ShouldThrowException_WhenApiCallFails()
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
            var result = await _newsApiClient.GetNewsAsync("test");

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