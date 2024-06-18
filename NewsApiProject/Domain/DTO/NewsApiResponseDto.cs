namespace Domain.DTO
{
    public class NewsApiResponseDto
    {
        public string Status { get; set; }
        public int? TotalResults { get; set; }
        public List<ArticleDto> Articles { get; set; }
    }
}
