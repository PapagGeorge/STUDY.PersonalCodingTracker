using Application.Interfaces;
using Domain.Entities;
using System.Text.Json;

namespace Application
{
    public class OMDbAccess : IOMDbAccess
    {
        private readonly HttpClient _httpClient;

        public OMDbAccess(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Movie> GetMovieByImdbIdAsync(string movieId)
        {
            _httpClient.BaseAddress = new Uri("http://www.omdbapi.com");
            var response = await _httpClient.GetAsync($"?apikey=e2ba4351&i={movieId}");
            Movie movie = new Movie();

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                movie = JsonSerializer.Deserialize<Movie>(responseBody, options);

            }

            return movie;
        }
    }
}
