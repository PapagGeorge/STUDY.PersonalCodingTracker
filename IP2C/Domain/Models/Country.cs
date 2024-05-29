namespace Domain.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string TwoLetterCode { get; set; } = string.Empty;
        public string ThreeLetterCode { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Country(string name, string twoLetterCode, string threeLetterCode, DateTime createdAt)
        {
            Name = name ?? string.Empty;
            TwoLetterCode = twoLetterCode ?? string.Empty;
            ThreeLetterCode = threeLetterCode ?? string.Empty;
            CreatedAt = createdAt;
        }

        public static Country Create(string name, string twoLetterCode, string threeLetterCode, DateTime createdAt)
        {
            return new Country(name, twoLetterCode, threeLetterCode, createdAt);
        }
    }
}
