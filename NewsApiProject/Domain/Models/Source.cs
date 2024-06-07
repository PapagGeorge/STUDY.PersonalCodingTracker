using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Source
    {
        public int Id { get; set; }
        [JsonPropertyName("id")]
        public string SourceId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
