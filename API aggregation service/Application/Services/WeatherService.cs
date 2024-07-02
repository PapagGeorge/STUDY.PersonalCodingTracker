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
        public async Task<WeatherData> GetWeatherApiResponseAsync(string countryCode, string cityName)
        {
            var weatherApiClientResponse = await _weatherApiClient.GetWeatherAsync(countryCode, cityName);
            return weatherApiClientResponse;
        }
    }
}
