using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Rating
    {
        [JsonPropertyName("Source")]
        public string Source { get; init; }
        [JsonPropertyName("Value")]
        public string Value { get; init; }
    }
}
