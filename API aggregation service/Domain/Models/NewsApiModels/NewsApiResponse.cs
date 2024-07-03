using System.Text.Json.Serialization;

namespace Domain.Models.NewsApiModels
{
    public class NewsApiResponse
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("totalResults")]
        public int? TotalResults { get; set; }
        [JsonPropertyName("articles")]
        public List<Article> Articles { get; set; }
    }
}
