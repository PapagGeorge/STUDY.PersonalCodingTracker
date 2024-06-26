﻿
namespace Domain.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string UrlToImage { get; set; } = string.Empty;
        public DateTime PublishedAt { get; set; }
        public string Content { get; set; } = string.Empty;
        public int SourceId { get; set; }
        public Source Source { get; set; } = new();
    }
}
