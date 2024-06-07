using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Source
    {
        public int Id { get; set; } // Database primary key, not needed in JSON
        [JsonPropertyName("id")]
        public string SourceId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }

        // Collection navigation property for related articles
        public ICollection<Article> Articles { get; set; }
    }
}
