using Application.Constants;
using Application.Interfaces;
using Domain.Models.NewsApiModels;
using Domain.Models.WeatherBitApi;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Application.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherApiClient _weatherApiClient;
        private readonly IDistributedCache _distributedCache;

        public WeatherService(IWeatherApiClient weatherApiClient, IDistributedCache distributedCache)
        {
            _weatherApiClient = weatherApiClient;
            _distributedCache = distributedCache;
        }
        public async Task <IEnumerable<WeatherData>> GetWeatherApiResponseAsync(bool ascending, string sortBy = "temperature")
        {
            //Attempt to fetch cached response
            var cacheKey = GetCacheKey(ascending, sortBy);
            var weatherApiClientResponse = await _distributedCache.GetRecordAsync<IEnumerable<WeatherData>>(cacheKey, GetJsonSerializerOptions());

            if(weatherApiClientResponse == null)
            {
                //Fetch from API if not in cache
                weatherApiClientResponse = await FetchWeatherDataFromApi();

                if(weatherApiClientResponse == null)
                {
                    //Handle case where API or fallback mechanisms didn't return valid data
                    weatherApiClientResponse = CreateDefaultWeatherResponse();
                }
                else
                {
                    await _distributedCache.SetRecordAsync(cacheKey, weatherApiClientResponse);
                }
            }

            if(sortBy == "temperature")
            {
                weatherApiClientResponse = ascending
                    ? weatherApiClientResponse.OrderBy(w => w.DataList.FirstOrDefault()?.Temp).ToList()
                    : weatherApiClientResponse.OrderByDescending(w => w.DataList.FirstOrDefault()?.Temp).ToList();
            }

            return weatherApiClientResponse;
        }

        private static string GetCacheKey(bool ascending, string sortBy)
        {
            var keyParts = new List<string>();

            if (!string.IsNullOrEmpty(sortBy))
            {
                keyParts.Add($"sortBy_{sortBy}");
            }

            keyParts.Add($"asc_{ascending}");

            if (keyParts.Count == 0)
            {
                return "default_weather_cache_key";
            }

            var cacheKey = string.Join("_", keyParts);

            return cacheKey;
        }

        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        private IEnumerable<WeatherData> CreateDefaultWeatherResponse()
        {
            var defaultWeatherData = new List<WeatherData>()
            {
                new WeatherData
                {
                    DataList = new List<Data>
                    {
                        new Data
                        {
                            WindCdir = "N/A",
                            Rh = -1,
                            Pod = "N/A",
                            Lon = -1,
                            Pres = -1,
                            Timezone = "N/A",
                            ObTime = "N/A",
                            CountryCode = "N/A",
                            Clouds = -1,
                            Vis = -1,
                            WindSpd = -1,
                            Gust = -1,
                            WindCdirFull = "N/A",
                            AppTemp = -1,
                            StateCode = "N/A",
                            Ts = 0,
                            HAngle = -1,
                            Dewpt = -1,
                            Weather = new Weather
                            {
                                Icon = "N/A",
                                Code = -1,
                                Description = "Error: No data available"
                            },
                            Uv = -1,
                            Aqi = -1,
                            Station = "N/A",
                            Sources = new List<string> { "N/A" },
                            WindDir = -1,
                            ElevAngle = -1,
                            Datetime = "N/A",
                            Precip = -1,
                            Ghi = -1,
                            Dni = -1,
                            Dhi = -1,
                            SolarRad = -1,
                            CityName = "Unknown",
                            Sunrise = "N/A",
                            Sunset = "N/A",
                            Temp = -999,
                            Lat = -1,
                            Slp = -1
                        }
                    },
                    Count = 1
                }
            };
            return defaultWeatherData;
        }

        private async Task<IEnumerable<WeatherData>> FetchWeatherDataFromApi()
        {
            var cityList = CityNames.GetCityNames();
            var tasks = cityList.Select(city => _weatherApiClient.GetWeatherAsync(CountryCodes.Greece, city)).ToArray();

            try
            {
                var weatherDataArray = await Task.WhenAll(tasks);
                return weatherDataArray;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
