namespace Domain.Models
{
    public class NewsApiResponse
    {
        public string Status { get; init; }
        public int TotalResults { get; init; }
        public List<Article> Articles { get; init; }
    }
}
