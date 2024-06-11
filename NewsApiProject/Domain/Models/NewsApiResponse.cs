using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class NewsApiResponse
    {
        public int NewsApiResponseId { get; init; } // Database primary key, not needed in JSON
        [JsonPropertyName("status")]
        public string? Status { get; init; }
        [JsonPropertyName("totalResults")]
        public int? TotalResults { get; init; }
        [JsonPropertyName("articles")]
        public List<Article> Articles { get; init; }
    }
}
