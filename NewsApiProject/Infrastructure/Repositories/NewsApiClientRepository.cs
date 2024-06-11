using Domain.Models;
using System.Text.Json;
using Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class NewsApiClientRepository : INewsApiClientRepository
    {
        private const string BaseUrl = "https://newsapi.org/v2/everything";
        private const string ApiKey = "5e666310837a4d05ab612db16657b36e";
        private readonly HttpClient _httpClient;
        private readonly ILogger<NewsApiClientRepository> _logger;

        public NewsApiClientRepository(HttpClient httpClient, ILogger<NewsApiClientRepository> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<NewsApiResponse> GetNewsAsync(string keyword)
        {
            var url = $"{BaseUrl}?q={keyword}&apiKey={ApiKey}";
            _logger.LogInformation($"Fetching news from URL: {url}", url);

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.UserAgent.ParseAdd("NewsApiProject/1.0");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Received response: {json}", json);

                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var newsApiResponse = JsonSerializer.Deserialize<NewsApiResponse>(json, options);
                return newsApiResponse;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Failed to fetch news. Status Code: {StatusCode}, Response: {ErrorContent}", response.StatusCode, errorContent);
                throw new Exception("Unable to fetch news from the API");
            }
        }
    }
}
