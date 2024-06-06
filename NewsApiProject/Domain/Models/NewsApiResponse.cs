namespace Domain.Models
{
    public class NewsApiResponse
    {
        public int NewsApiResponseId { get; init; }
        public string Status { get; init; }
        public int TotalResults { get; init; }
        public List<Article> Articles { get; init; }
    }
}
