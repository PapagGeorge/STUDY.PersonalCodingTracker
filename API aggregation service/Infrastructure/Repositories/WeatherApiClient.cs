using Application.Interfaces;
using Domain.Models.NewsApiModels;
using Domain.Models.WeatherBitApi;
using System.Text.Json;
using Polly;
using Polly.Wrap;
using Polly.CircuitBreaker;
using System.Diagnostics;
using Domain.Models.RequestStatistics;
using Microsoft.Extensions.Configuration;


namespace Infrastructure.Repositories
{
    public class WeatherApiClient : IWeatherApiClient
    {
        private readonly string BaseUrl;
        private readonly string ApiKey;
        private readonly HttpClient _httpClient;
        private readonly AsyncPolicyWrap<HttpResponseMessage> _retryAndBreakerPolicy;
        private readonly IRequestStatisticsRepository _requestStatisticsRepository;

        public WeatherApiClient(HttpClient httpClient, IRequestStatisticsRepository requestStatisticsRepository, IConfiguration configuration)
        {
            BaseUrl = configuration["ApiSettings:WeatherBitUrl"];
            ApiKey = configuration["ApiSettings:WeatherBitApiKey"];
            _httpClient = httpClient;

            //Define retry policy: Retry up to 3 times with exponential backoff
            var retryPolicy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            //Define circuit breaker policy: Break circuit after 3 consecutive failures, for 30 seconds
            var circuitBreakerPolicy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));

            //Combine retry and circuit breaker policies
            _retryAndBreakerPolicy = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);

            _requestStatisticsRepository = requestStatisticsRepository;
        }
        public async Task<WeatherData> GetWeatherAsync(string countryCode, string cityName)
        {
            var stopwatch = Stopwatch.StartNew();

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
            finally
            {
                stopwatch.Stop();
                _requestStatisticsRepository.AddRequestStatistic(new RequestStatistic
                {
                    ApiName = "WeatherApi",
                    ResponseTime = stopwatch.ElapsedMilliseconds,
                    Timestamp = DateTime.UtcNow
                });
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
