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
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class AstronomyPictureClient : IAstronomyPictureClient
    {
        private readonly HttpClient _httpClient;
        private readonly string BaseUrl;
        private readonly string ApiKey;
        private readonly AsyncPolicyWrap<HttpResponseMessage> _retryAndBreakerPolicy;
        private readonly IRequestStatisticsRepository _requestStatisticsRepository;

        public AstronomyPictureClient(HttpClient httpClient, IRequestStatisticsRepository requestStatisticsRepository, IConfiguration configuration)
        {
            BaseUrl = configuration["ApiSettings:NasaApiBaseUrl"];
            ApiKey = configuration["ApiSettings:NasaApiKey"];
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
                return null; // Return null if Status Code not Successfull
            }
        }
    }
}
