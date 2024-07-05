using Application.Constants;
using Application.Interfaces;
using Application.Services;
using Domain.Models.WeatherBitApi;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System.Text;
using System.Text.Json;

namespace ApiAggregationService.Tests.ServicesTests
{
    [TestFixture]
    public class WeatherServiceTests
    {
        private Mock<IWeatherApiClient> _mockWeatherApiClient;
        private Mock<IDistributedCache> _mockDistributedCache;
        private WeatherService _weatherService;

        [SetUp]
        public void SetUp()
        {
            _mockWeatherApiClient = new Mock<IWeatherApiClient>();
            _mockDistributedCache = new Mock<IDistributedCache>();
            _weatherService = new WeatherService(_mockWeatherApiClient.Object, _mockDistributedCache.Object);
        }

        [Test]
        public async Task GetWeatherApiResponseAsync_ReturnsDataFromCache_WhenCacheIsNotEmpty()
        {
            // Arrange
            var cachedWeatherData = GetMockWeatherData();
            var cacheKey = "sortBy_temperature_asc_True";
            var json = JsonSerializer.Serialize(cachedWeatherData, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            _mockDistributedCache.Setup(x => x.GetAsync(cacheKey, default)).ReturnsAsync(Encoding.UTF8.GetBytes(json));

            // Act
            var result = await _weatherService.GetWeatherApiResponseAsync(true, "temperature");

            // Assert
            Assert.AreEqual(cachedWeatherData.Count(), result.Count());
            _mockWeatherApiClient.Verify(x => x.GetWeatherAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task GetWeatherApiResponseAsync_FetchesDataFromApi_WhenCacheIsEmpty()
        {
            // Arrange
            var cityList = CityNames.GetCityNames();
            var weatherDataFromApi = GetMockWeatherData().ToArray();
            var cacheKey = "sortBy_temperature_asc_True";

            _mockDistributedCache.Setup(x => x.GetAsync(cacheKey, default)).ReturnsAsync((byte[])null);
            _mockWeatherApiClient.Setup(x => x.GetWeatherAsync(CountryCodes.Greece, It.IsAny<string>()))
                .ReturnsAsync((string countryCode, string city) => weatherDataFromApi.First(w => w.DataList.First().CityName == city));

            // Act
            var result = await _weatherService.GetWeatherApiResponseAsync(true, "temperature");

            // Assert
            Assert.AreEqual(cityList.Count(), result.Count());
            _mockWeatherApiClient.Verify(x => x.GetWeatherAsync(CountryCodes.Greece, It.IsAny<string>()), Times.Exactly(cityList.Count()));
        }

        [Test]
        public async Task GetWeatherApiResponseAsync_ReturnsSortedData_WhenAscendingIsTrue()
        {
            // Arrange
            var weatherDataFromApi = GetMockWeatherData();
            _mockDistributedCache.Setup(x => x.GetAsync(It.IsAny<string>(), default)).ReturnsAsync((byte[])null);
            _mockWeatherApiClient.Setup(x => x.GetWeatherAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(weatherDataFromApi.First);

            // Act
            var result = await _weatherService.GetWeatherApiResponseAsync(true, "temperature");

            // Assert
            Assert.IsTrue(result.First().DataList.First().Temp <= result.Last().DataList.First().Temp);
        }

        [Test]
        public async Task GetWeatherApiResponseAsync_ReturnsDefaultData_WhenApiAndCacheFail()
        {
            // Arrange
            _mockDistributedCache.Setup(x => x.GetAsync(It.IsAny<string>(), default)).ReturnsAsync((byte[])null);
            _mockWeatherApiClient.Setup(x => x.GetWeatherAsync(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception());

            // Act
            var result = await _weatherService.GetWeatherApiResponseAsync(true, "temperature");

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Error: No data available", result.First().DataList.First().Weather.Description);
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

    }
}
