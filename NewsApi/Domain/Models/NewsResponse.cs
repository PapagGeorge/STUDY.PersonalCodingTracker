namespace Domain.Models
{
    public class NewsResponse
    {
        public string Status { get; set; }
        public int TotalResults { get; set; }
        public List<Article> Articles { get; set; }

        public NewsResponse(string status, int totalResults, List<Article> articles)
        {
            Status = status;
            TotalResults = totalResults;
            Articles = articles;
        }

        public static NewsResponse Create(string status, int totalResults, List<Article> articles)
        {
            return new NewsResponse(status, totalResults, articles);
        }
    }
}
