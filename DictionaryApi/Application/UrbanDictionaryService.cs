using Application.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Application
{
    public class UrbanDictionaryService : IUrbanDictionaryService
    {
        private readonly HttpClient _httpClient;

        public UrbanDictionaryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task <string> GetDefinitionAsync(string term)
        {

            var url = $"https://mashape-community-urban-dictionary.p.rapidapi.com/define?term={term}";

            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", "866172a449mshf79acb8d04add64p16bbaejsnd43b5b49eec5");
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", "mashape-community-urban-dictionary.p.rapidapi.com");

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            else
            {
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
            }
        }
    }
}
