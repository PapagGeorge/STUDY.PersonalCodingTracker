using System.Text.Json.Serialization;

namespace Domain
{
    public class DefinitionModel
    {

        [JsonPropertyName("definition")]
        public string Definition { get; set; }

        [JsonPropertyName("permalink")]
        public string Permalink { get; set; }

        [JsonPropertyName("thumbs_up")]
        public int ThumbsUp { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("word")]
        public string Word { get; set; }

        [JsonPropertyName("defid")]
        public long DefId { get; set; }

        [JsonPropertyName("current_vote")]
        public string CurrentVote { get; set; }

        [JsonPropertyName("written_on")]
        public DateTime WrittenOn { get; set; }

        [JsonPropertyName("example")]
        public string Example { get; set; }

        [JsonPropertyName("thumbs_down")]
        public int ThumbsDown { get; set; }
    }
}

