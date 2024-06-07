using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Source
    {
        [Key]
        [Column("Unique")]
        public int Unique { get; set; } // Database primary key, not needed in JSON
        [JsonPropertyName("id")]
        public string? SourceId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }

        // Collection navigation property for related articles
        public ICollection<Article> Articles { get; set; }
    }
}
