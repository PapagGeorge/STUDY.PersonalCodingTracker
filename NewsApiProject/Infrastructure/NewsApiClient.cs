using Domain.Models;
using System.Text.Json;
using Application.Interfaces;

namespace Infrastructure
{
    public class NewsApiClient : INewsApiClient
    {
        private const string BaseUrl = "https://newsapi.org/v2/everything";
        private const string ApiKey = "5e666310837a4d05ab612db16657b36e";
        private readonly HttpClient _httpClient;

        public NewsApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<NewsApiResponse> GetNewsAsync(string keyword)
        {
            var url = $"{BaseUrl}?q={keyword}&apiKey={ApiKey}";
            var response = await _httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var newsApiResponse = JsonSerializer.Deserialize<NewsApiResponse>(json, options);
                return newsApiResponse;
            }
            throw new Exception("Unable to fetch news from the API");
        }
    }
}
