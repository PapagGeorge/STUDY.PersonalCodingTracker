using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Movie
    {
        [JsonPropertyName("Title")]
        public string Title { get; init; }
        [JsonPropertyName("Year")]
        public string Year { get; init; }
        [JsonPropertyName("Rated")]
        public string Rated { get; init; }
        [JsonPropertyName("Released")]
        public string Released { get; init; }
        [JsonPropertyName("Runtime")]
        public string Runtime { get; init; }
        [JsonPropertyName("Genre")]
        public string Genre { get; init; }
        [JsonPropertyName("Director")]
        public string Director { get; init; }
        [JsonPropertyName("Writer")]
        public string Writer { get; init; }
        [JsonPropertyName("Actor")]
        public string Actors { get; init; }
        [JsonPropertyName("Plot")]
        public string Plot { get; init; }
        [JsonPropertyName("Language")]
        public string Language { get; init; }
        [JsonPropertyName("Country")]
        public string Country { get; init; }
        [JsonPropertyName("Awards")]
        public string Awards { get; init; }
        [JsonPropertyName("Poster")]
        public string Poster { get; init; }
        [JsonPropertyName("Ratings")]
        public List<Rating> Ratings { get; init; }
        [JsonPropertyName("Metascore")]
        public string Metascore { get; init; }
        [JsonPropertyName("imdbRating")]
        public string ImdbRating { get; init; }
        [JsonPropertyName("imdbVotes")]
        public string ImdbVotes { get; init; }
        [JsonPropertyName("imdbID")]
        public string ImdbID { get; init; }
        [JsonPropertyName("Type")]
        public string Type { get; init; }
        [JsonPropertyName("DVD")]
        public string DVD { get; init; }
        [JsonPropertyName("BoxOffice")]
        public string BoxOffice { get; init; }
        [JsonPropertyName("Production")]
        public string Production { get; init; }
        [JsonPropertyName("Website")]
        public string Website { get; init; }
        [JsonPropertyName("Response")]
        public string Response { get; init; }
    }
}
