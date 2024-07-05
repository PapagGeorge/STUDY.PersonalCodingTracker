using Application.Constants;
using Application.Interfaces;
using Application.Services;
using Domain.Models.AstronomyPictureModel;
using Domain.Models.NewsApiModels;
using Domain.Models.WeatherBitApi;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAggregationService.Tests.ServicesTests
{
    [TestFixture]
    public class AggregateServiceTests
    {
        private Mock<IWeatherService> _mockWeatherService;
        private Mock<INewsService> _mockNewsService;
        private Mock<IAstronomyPictureService> _mockAstronomyPictureService;
        private AggregateService _aggregateService;

        [SetUp]
        public void SetUp()
        {
            _mockWeatherService = new Mock<IWeatherService>();
            _mockNewsService = new Mock<INewsService>();
            _mockAstronomyPictureService = new Mock<IAstronomyPictureService>();
            _aggregateService = new AggregateService(_mockWeatherService.Object, _mockNewsService.Object, _mockAstronomyPictureService.Object);
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

        private IEnumerable<WeatherData> GetMockWeatherData()
        {
            var cityList = CityNames.GetCityNames();
            var weatherDataList = new List<WeatherData>();

            foreach (var city in cityList)
            {
                weatherDataList.Add(new WeatherData
                {
                    DataList = new List<Data>
            {
                new Data
                {
                    WindCdir = "N",
                    Rh = 50,
                    Pod = "d",
                    Lon = 23.7275,
                    Pres = 1012,
                    Timezone = "Europe/Athens",
                    ObTime = "2023-07-05 12:00",
                    CountryCode = "GR",
                    Clouds = 20,
                    Vis = 10,
                    WindSpd = 3.6,
                    Gust = 5.1,
                    WindCdirFull = "north",
                    AppTemp = 30,
                    StateCode = "I",
                    Ts = 1625487600,
                    HAngle = 15,
                    Dewpt = 15,
                    Weather = new Weather
                    {
                        Icon = "c01d",
                        Code = 800,
                        Description = "Clear sky"
                    },
                    Uv = 7,
                    Aqi = 30,
                    Station = "ST001",
                    Sources = new List<string> { "source1", "source2" },
                    WindDir = 0,
                    ElevAngle = 60,
                    Datetime = "2023-07-05:12",
                    Precip = 0,
                    Ghi = 1000,
                    Dni = 800,
                    Dhi = 100,
                    SolarRad = 900,
                    CityName = city,
                    Sunrise = "06:00",
                    Sunset = "20:30",
                    Temp = 30,
                    Lat = 37.9838,
                    Slp = 1015
                }
            },
                    Count = 1
                });
            }

            return weatherDataList;
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

        [Test]
        public async Task GetAggregateData_CallsAllServices()
        {
            // Arrange
            var newsKeyword = "testKeyword";
            _mockNewsService.Setup(x => x.GetNewsApiResponseAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                            .ReturnsAsync(GetMockNewsApiResponse());
            _mockWeatherService.Setup(x => x.GetWeatherApiResponseAsync(It.IsAny<bool>(), It.IsAny<string>()))
                               .ReturnsAsync(GetMockWeatherData());
            _mockAstronomyPictureService.Setup(x => x.GetAstronomyPictures(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                                        .ReturnsAsync(GetMockAstronomyPictures());

            // Act
            var result = await _aggregateService.GetAggregateData(newsKeyword, true);

            // Assert
            _mockNewsService.Verify(x => x.GetNewsApiResponseAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
            _mockWeatherService.Verify(x => x.GetWeatherApiResponseAsync(It.IsAny<bool>(), It.IsAny<string>()), Times.Once);
            _mockAstronomyPictureService.Verify(x => x.GetAstronomyPictures(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public async Task GetAggregateData_AggregatesDataCorrectly()
        {
            // Arrange
            var newsKeyword = "testKeyword";
            _mockNewsService.Setup(x => x.GetNewsApiResponseAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                            .ReturnsAsync(GetMockNewsApiResponse());
            _mockWeatherService.Setup(x => x.GetWeatherApiResponseAsync(It.IsAny<bool>(), It.IsAny<string>()))
                               .ReturnsAsync(GetMockWeatherData());
            _mockAstronomyPictureService.Setup(x => x.GetAstronomyPictures(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                                        .ReturnsAsync(GetMockAstronomyPictures());

            // Act
            var result = await _aggregateService.GetAggregateData(newsKeyword, true);

            // Assert
            Assert.IsNotNull(result.NewsApiResponse);
            Assert.IsNotNull(result.WeatherData);
            Assert.IsNotNull(result.AstronomyPicture);
            Assert.AreEqual(2, result.NewsApiResponse.TotalResults);
            Assert.AreEqual(CityNames.GetCityNames().Count(), result.WeatherData.Count());
            Assert.AreEqual(2, result.AstronomyPicture.Count());
        }


        [Test]
        public async Task GetAggregateData_HandlesDefaultData_WhenServicesReturnNoData()
        {
            // Arrange
            var newsKeyword = "testKeyword";
            _mockNewsService.Setup(x => x.GetNewsApiResponseAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                            .ReturnsAsync(new NewsApiResponse { Status = "error", TotalResults = 0, Articles = new List<Article>() });
            _mockWeatherService.Setup(x => x.GetWeatherApiResponseAsync(It.IsAny<bool>(), It.IsAny<string>()))
                               .ReturnsAsync(new List<WeatherData>());
            _mockAstronomyPictureService.Setup(x => x.GetAstronomyPictures(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                                        .ReturnsAsync(new List<AstronomyPicture>());

            // Act
            var result = await _aggregateService.GetAggregateData(newsKeyword, true);

            // Assert
            Assert.IsNotNull(result.NewsApiResponse);
            Assert.IsNotNull(result.WeatherData);
            Assert.IsNotNull(result.AstronomyPicture);
            Assert.AreEqual(0, result.NewsApiResponse.TotalResults);
            Assert.AreEqual(0, result.WeatherData.Count());
            Assert.AreEqual(0, result.AstronomyPicture.Count());
        }

    }
}
