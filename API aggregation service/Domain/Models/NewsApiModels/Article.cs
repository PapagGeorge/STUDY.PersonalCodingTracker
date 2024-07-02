using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models.NewsApiModels
{
    public class Article
    {
        [JsonPropertyName("author")]
        public string? Author { get; init; }
        [JsonPropertyName("title")]
        public string? Title { get; init; }
        [JsonPropertyName("description")]
        public string? Description { get; init; }
        [JsonPropertyName("url")]
        public string? Url { get; init; }
        [JsonPropertyName("urlToImage")]
        public string? UrlToImage { get; init; }
        [JsonPropertyName("publishedAt")]
        public DateTime? PublishedAt { get; init; }
        [JsonPropertyName("content")]
        public string? Content { get; init; }
        // This property maps the nested source object in JSON
        [JsonPropertyName("source")]
        public Source Source { get; set; }
    }
}
