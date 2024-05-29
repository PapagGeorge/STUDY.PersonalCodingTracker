namespace Application.Extension
{
    public static class HttpClientExtensions
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task <List<string>> GetHttpClientAsync(this string url)
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Windows application");

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };

            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            
            if(response.IsSuccessStatusCode)
            {
                if(json.Contains("WRONG INPUT", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new HttpRequestException($"Request was successful but failed to load data {json}");
                }

                return (await response.Content.ReadAsStringAsync()).Split(';').ToList();
            }
            else
            {
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
            }
        }
    }
}
