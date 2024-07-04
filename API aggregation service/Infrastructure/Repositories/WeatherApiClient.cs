using Application.Interfaces;
using Domain.Models.NewsApiModels;
using Domain.Models.WeatherBitApi;
using System.Text.Json;
using Polly;
using Polly.Wrap;
using Polly.CircuitBreaker;

namespace Infrastructure.Repositories
{
    public class WeatherApiClient : IWeatherApiClient
    {
        private const string BaseUrl = "http://api.weatherbit.io/v2.0/current";
        private const string ApiKey = "c969571a57e44642be74e4a2373949bd";
        private readonly HttpClient _httpClient;
        private readonly AsyncPolicyWrap<HttpResponseMessage> _retryAndBreakerPolicy;

        public WeatherApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            //Define retry policy: Retry up to 3 times with exponential backoff
            var retryPolicy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            //Define circuit breaker policy: Break circuit after 3 consecutive failures, for 30 seconds
            var circuitBreakerPolicy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));

            //Combine retry and circuit breaker policies
            _retryAndBreakerPolicy = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);
        }
        public async Task<WeatherData> GetWeatherAsync(string countryCode, string cityName)
        {
            var url = $"{BaseUrl}?&city={cityName}&country={countryCode}&key={ApiKey}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            HttpResponseMessage response;

            try
            {
                //Execute request with retry and circuit breaker policies
                response = await _retryAndBreakerPolicy.ExecuteAsync(() => _httpClient.SendAsync(request));
            }
            catch(BrokenCircuitException)
            {
                throw new Exception("Circuit breaker is open. Unable to fetch news from the API");
            }
            catch(Exception ex)
            {
                throw new Exception("An error occured while fetching news from the API", ex);
            }

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
                throw new Exception($"Unable to fetch weather from the API. Status Code: {response.StatusCode}");
            }
        }
    }
}
