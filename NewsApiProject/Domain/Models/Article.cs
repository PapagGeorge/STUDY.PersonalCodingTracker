using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
    
{
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArticleId { get; set; }
        [JsonPropertyName("author")]
        public string Author { get; init; }
        [JsonPropertyName("title")]
        public string Title { get; init; }
        [JsonPropertyName("description")]
        public string Description { get; init; }
        [JsonPropertyName("url")]
        public string Url { get; init; }
        [JsonPropertyName("urlToImage")]
        public string UrlToImage { get; init; }
        [JsonPropertyName("publishedAt")]
        public DateTime PublishedAt { get; init; }
        [JsonPropertyName("content")]
        public string Content { get; init; }

        // These properties are for database use only and should not be deserialized
        public int SourceId { get; init; }
        public string SourceName { get; set; }
        public int NewsApiResponseId { get; init; }
        public NewsApiResponse NewsApiResponse { get; init; }

        // This property maps the nested source object in JSON
        [JsonPropertyName("source")]
        public Source Source { get; set; }
    }
}
