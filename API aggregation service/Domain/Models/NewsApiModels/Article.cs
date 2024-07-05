using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models.NewsApiModels
{
    public class Article
    {
        [JsonPropertyName("author")]
        public string Author { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("urlToImage")]
        public string UrlToImage { get; set; }
        [JsonPropertyName("publishedAt")]
        public DateTime? PublishedAt { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        // This property maps the nested source object in JSON
        [JsonPropertyName("source")]
        public Source Source { get; set; }
    }
}
