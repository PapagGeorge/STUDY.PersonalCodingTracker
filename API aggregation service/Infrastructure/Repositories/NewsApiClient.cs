using Application.Interfaces;
using Domain.Models.NewsApiModels;
using System.Text.Json;

namespace Infrastructure.Repositories
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
            var url = $"{BaseUrl}?q=\"{keyword}\"&apiKey={ApiKey}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.UserAgent.ParseAdd("NewsApiProject/1.0");

            var response = await _httpClient.SendAsync(request);

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
                throw new Exception("Unable to fetch news from the API");
            }
        }
    }
}
