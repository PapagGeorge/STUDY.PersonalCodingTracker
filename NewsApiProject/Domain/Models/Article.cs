namespace Domain.Models
{
    public class Article
    {
        public int ArticleId { get; init; } // Primary key for the database
        public int SourceId { get; init; } //foreign key referencing source table
        public string SourceName { get; set; }
        public int NewsApiResponseId { get; init; } //foreign key referencing NewsResponse table
        public string Author { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string Url { get; init; }
        public string UrlToImage { get; init; }
        public DateTime PublishedAt { get; init; }
        public string Content { get; init; }
        public Source Source { get; init; }
        public NewsApiResponse NewsApiResponse { get; set; }
    }
}
