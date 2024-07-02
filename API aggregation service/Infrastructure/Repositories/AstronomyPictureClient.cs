using Domain.Models.AstronomyPictureModel;
using Application.Interfaces;
using System;
using Domain.Models.NewsApiModels;
using System.Text.Json;

namespace Infrastructure.Repositories
{
    public class AstronomyPictureClient : IAstronomyPictureClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.nasa.gov/planetary/apod?";
        private const string ApiKey = "2yjMsve0FNfscrra6y4QnqFn1ObuDkrE5Fb73n5k";

        public AstronomyPictureClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<AstronomyPicture>> GetAstronomyPicturesAsync(string startDate = null, string endDate = null)
        {
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
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var astronomyPictureResponse = JsonSerializer.Deserialize<IEnumerable<AstronomyPicture>>(json, options);
                return astronomyPictureResponse;
            }

            else
            {
                throw new Exception("Unable to fetch news from the API");
            }
        }
    }
}
