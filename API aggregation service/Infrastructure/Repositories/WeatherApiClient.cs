using Application.Interfaces;
using Domain.Models.NewsApiModels;
using Domain.Models.WeatherBitApi;
using System.Text.Json;

namespace Infrastructure.Repositories
{
    public class WeatherApiClient : IWeatherApiClient
    {
        private const string BaseUrl = "http://api.weatherbit.io/v2.0/current";
        private const string ApiKey = "c969571a57e44642be74e4a2373949bd";
        private readonly HttpClient _httpClient;

        public WeatherApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<WeatherData> GetWeatherAsync(string countryCode, string cityName)
        {
            var url = $"{BaseUrl}?&city={cityName}&country={countryCode}&key={ApiKey}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var weatherApiResponse = JsonSerializer.Deserialize<WeatherData>(json, options);
                return weatherApiResponse;
            }
            else
            {
                throw new Exception("Unable to fetch news from the API");
            }
        }
    }
}
