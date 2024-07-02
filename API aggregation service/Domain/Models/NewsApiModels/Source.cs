using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models.NewsApiModels
{
    public class Source
    {
        [JsonPropertyName("id")]
        public string? SourceId { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
