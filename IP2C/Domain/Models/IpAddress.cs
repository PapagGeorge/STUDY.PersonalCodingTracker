namespace Domain.Models
{
    public class IpAddress
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Ip { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public IpAddress(int countryId, string ip, DateTime createdAt, DateTime updatedAt)
        {
            CountryId = countryId;
            Ip = ip;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public static IpAddress Create(int countryId, string ip, DateTime createdAt, DateTime updatedAt)
        {
            return new IpAddress(countryId, ip, createdAt, updatedAt);
        }
    }
}
