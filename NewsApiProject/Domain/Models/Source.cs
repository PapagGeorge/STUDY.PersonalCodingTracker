using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Source
    {
        [JsonPropertyName("id")]
        public string Id { get; init; }
        [JsonPropertyName("name")]
        public string Name { get; init; }
    }
}
