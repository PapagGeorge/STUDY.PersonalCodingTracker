using Application.Constants;
using Application.Interfaces;
using Domain.Models.NewsApiModels;

using Domain.Models.WeatherBitApi;

namespace Application.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherApiClient _weatherApiClient;

        public WeatherService(IWeatherApiClient weatherApiClient)
        {
            _weatherApiClient = weatherApiClient;
        }
        public async Task <IEnumerable<WeatherData>> GetWeatherApiResponseAsync(bool ascending, string sortBy = "temperature")
        {
            var cityList = CityNames.GetCityNames();
            List<WeatherData> weatherApiClientResponse = new List<WeatherData>();

            foreach (var city in cityList)
            {
                var cityApiClientResponse = await _weatherApiClient.GetWeatherAsync(CountryCodes.Greece, city);
                weatherApiClientResponse.Add(cityApiClientResponse);

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
    }
}
