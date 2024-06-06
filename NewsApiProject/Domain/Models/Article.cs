namespace Domain.Models
    using System.Text.Json.Serialization;
{
    public class Article
    {
        public int ArticleId { get; init; } // Primary key for the database
        public int SourceId { get; init; } //foreign key referencing source table
        public string SourceName { get; set; }
        public int NewsApiResponseId { get; init; } //foreign key referencing NewsResponse table
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
        public Source Source { get; init; }
        public NewsApiResponse NewsApiResponse { get; set; }
    }
}
