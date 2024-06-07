namespace Domain.DTO
{
    public class ArticleDto
    {
        public int ArticleId { get; set; }
        public string SourceId { get; set; }
        public string TextSourceId { get; set; }
        public string SourceName { get; set; }
        public int NewsApiResponseId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Content { get; set; }
    }
}
