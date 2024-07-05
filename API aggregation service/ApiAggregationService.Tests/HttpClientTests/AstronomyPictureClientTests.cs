using Domain.Models.NewsApiModels;
using Infrastructure.Repositories;
using Moq.Protected;
using Moq;
using Polly.CircuitBreaker;
using System.Net;
using System.Text.Json;
using Application.Interfaces;
using Domain.Models.AstronomyPictureModel;

namespace ApiAggregationService.Tests.HttpClientTests
{
    [TestFixture]
    public class AstronomyPictureClientTests
    {
        private Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private HttpClient _httpClient;
        private IAstronomyPictureClient _astronomyApiClient;

        [SetUp]
        public void Setup()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _astronomyApiClient = new AstronomyPictureClient(_httpClient);
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

            //Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _astronomyApiClient.GetAstronomyPicturesAsync(), "Circuit Breaker is open. Unable to fetch news from the API.");

        }

        [Test]
        public void GetAstronomyPicturesAsync_ShouldThrowException_WhenApiCallFails()
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
            Assert.ThrowsAsync<Exception>(async () => await _astronomyApiClient.GetAstronomyPicturesAsync(), "An error occured while fetching news from the API");
        }

        [TearDown]
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
