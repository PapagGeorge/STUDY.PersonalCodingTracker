using Application.Interfaces;
using Domain.Models.NewsApiModels;
using System.Text.Json;
using Polly;
using Polly.CircuitBreaker;
using Polly.Wrap;
using System.Diagnostics;
using Domain.Models.RequestStatistics;
using Microsoft.Extensions.Configuration;


namespace Infrastructure.Repositories
{
    public class NewsApiClient : INewsApiClient
    {
        private readonly string BaseUrl;
        private readonly string ApiKey;
        private readonly HttpClient _httpClient;
        private readonly AsyncPolicyWrap<HttpResponseMessage> _retryAndBreakerPolicy;
        private readonly IRequestStatisticsRepository _requestStatisticsRepository;

        public NewsApiClient(HttpClient httpClient, IRequestStatisticsRepository requestStatisticsRepository, IConfiguration configuration)
        {
            BaseUrl = configuration["ApiSettings:NewsApiBaseUrl"];
            ApiKey = configuration["ApiSettings:NewsApiKey"];
            _httpClient = httpClient;

            //Define retry policy: Retry up to 3 times with exponential backoff
            var retryPolicy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            //Define cirquit breaker policy: Break circuit after 3 concecutive failures, for 30 seconds
            var circuitBreakerPolicy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));

            //Combine retry and circuit breaker policies
            _retryAndBreakerPolicy = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);

            _requestStatisticsRepository = requestStatisticsRepository;
        }

        public async Task<NewsApiResponse> GetNewsAsync(string keyword)
        {
            var stopwatch = Stopwatch.StartNew();

            var url = $"{BaseUrl}?q=\"{keyword}\"&apiKey={ApiKey}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.UserAgent.ParseAdd("NewsApiProject/1.0");

            HttpResponseMessage response;

            try
            {
                response = await _retryAndBreakerPolicy.ExecuteAsync(() => _httpClient.SendAsync(request));
            }
            catch(BrokenCircuitException)
            {
                return null; // Return null if circuit breaker is open
            }
            catch(Exception ex)
            {
                return null; // Return null for any other exceptions
            }
            finally
            {
                stopwatch.Stop();
                _requestStatisticsRepository.AddRequestStatistic(new RequestStatistic
                {
                    ApiName = "NewsApi",
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

                var newsApiResponse = JsonSerializer.Deserialize<NewsApiResponse>(json, options);
                return newsApiResponse;
            }

            else
            {
                return null; // Return null if Status Code not Successfull
            }
        }
    }
}
