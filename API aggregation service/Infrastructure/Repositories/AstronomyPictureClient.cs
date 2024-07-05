using Domain.Models.AstronomyPictureModel;
using Application.Interfaces;
using System;
using Domain.Models.NewsApiModels;
using System.Text.Json;
using Polly;
using Polly.CircuitBreaker;
using Polly.Wrap;
using System.Diagnostics;
using Domain.Models.RequestStatistics;

namespace Infrastructure.Repositories
{
    public class AstronomyPictureClient : IAstronomyPictureClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.nasa.gov/planetary/apod?";
        private const string ApiKey = "2yjMsve0FNfscrra6y4QnqFn1ObuDkrE5Fb73n5k";
        private readonly AsyncPolicyWrap<HttpResponseMessage> _retryAndBreakerPolicy;
        private readonly IRequestStatisticsRepository _requestStatisticsRepository;

        public AstronomyPictureClient(HttpClient httpClient, IRequestStatisticsRepository requestStatisticsRepository)
        {
            _httpClient = httpClient;

            //Define retry policy: Retry up to 3 times with exponential backoff
            var retryPolicy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            //Define circuit breaker policy: Break circuit after 3 concecutive failures, for 30 seconds
            var circuitBreakerPolicy = Policy.HandleResult<HttpResponseMessage>(r => r.IsSuccessStatusCode)
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));

            //Combine retry and circuit breaker policies
            _retryAndBreakerPolicy = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);

            _requestStatisticsRepository = requestStatisticsRepository;
        }
        public async Task<IEnumerable<AstronomyPicture>> GetAstronomyPicturesAsync(string startDate = null, string endDate = null)
        {
            var stopwatch = Stopwatch.StartNew();
            string url;

            if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
            {
                url = $"{BaseUrl}api_key={ApiKey}";
            }
            else
            {
                url = $"{BaseUrl}api_key={ApiKey}&start_date={startDate}&end_date={endDate}";
            }

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            HttpResponseMessage response;
            try
            {
                response = await _retryAndBreakerPolicy.ExecuteAsync(() => _httpClient.SendAsync(request));
            }
            catch (BrokenCircuitException)
            {
                throw new Exception("Circuit breaker is open. Unable to fetch news from the API");
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occured while fetching news from the API, {ex}");
            }
            finally
            {
                stopwatch.Stop();
                _requestStatisticsRepository.AddRequestStatistic(new RequestStatistic
                {
                    ApiName = "AstronomyApi",
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

                IEnumerable<AstronomyPicture> astronomyPictureResponse;

                if (json.TrimStart().StartsWith("{"))
                {
                    var singleItem = JsonSerializer.Deserialize<AstronomyPicture>(json, options);
                    astronomyPictureResponse = new List<AstronomyPicture> { singleItem };
                }

                else
                {
                    astronomyPictureResponse = JsonSerializer.Deserialize<IEnumerable<AstronomyPicture>>(json, options);
                }

                return astronomyPictureResponse;
            }

            else
            {
                throw new Exception($"Failed to fetch astronomy pictures from API. Statuc code: {response.StatusCode}");
            }
        }
    }
}
