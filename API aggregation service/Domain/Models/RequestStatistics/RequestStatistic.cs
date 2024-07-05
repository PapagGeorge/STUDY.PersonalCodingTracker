namespace Domain.Models.RequestStatistics
{
    public class RequestStatistic
    {
        public string ApiName { get; set; }
        public long ResponseTime { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
