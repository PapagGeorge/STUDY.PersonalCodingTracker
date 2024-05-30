namespace Domain.Models
{
    public class Article
    {
        public Source Source { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Content { get; set; }

        public Article(Source source, string author, string title, string description, string url, string urlToImage,
            DateTime publishedAt, string content)
        {
            Source = source;
            Author = author;
            Title = title;
            Description = description;
            Url = url;
            UrlToImage = urlToImage;
            PublishedAt = publishedAt;
            Content = content;
        }

        public static Article Create(Source source, string author, string title, string description, string url, string urlToImage,
            DateTime publishedAt, string content)
        {
            return new Article(source, author, title, description, url, urlToImage, publishedAt, content);
        }
    }
}
