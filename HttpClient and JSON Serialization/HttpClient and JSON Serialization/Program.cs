using System.Text.Json.Serialization;
using System.Text.Json;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GetCatFacts().GetAwaiter().GetResult();
        }

        public static async Task GetCatFacts()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://cat-fact.herokuapp.com");

            try
            {
                var response = await client.GetAsync("/facts");

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    await Console.Out.WriteLineAsync(responseBody + "\n\n");

                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    var facts = JsonSerializer.Deserialize<List<Fact>>(responseBody, options);

                    foreach (var item in facts)
                    {
                        await Console.Out.WriteLineAsync($"Id: {item.Id}\n" +
                            $"Status_Verified: {item.Status.Verified}\n" +
                            $"Status_SentCount: {item.Status.SentCount}\n" +
                            $"User: {item.User}\n" +
                            $"Text: {item.Text}\n" +
                            $"Version_Number: {item.VersionNumber}\n" +
                            $"Source: {item.Source}\n" +
                            $"Updated_At: {item.UpdatedAt}\n" +
                            $"Type: {item.Type}\n" +
                            $"Created_At: {item.CreatedAt}\n" +
                            $"Deleted: {item.Deleted}\n" +
                            $"Used: {item.Used}\n");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }

}

public class Fact
{
    [JsonPropertyName("status")]
    public Status Status { get; init; } = new();
    [JsonPropertyName("_id")]
    public string Id { get; init; } = string.Empty;
    [JsonPropertyName("user")]
    public string User { get; init; } = string.Empty;
    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;
    [JsonPropertyName("__v")]
    public int VersionNumber { get; init; }
    [JsonPropertyName("source")]
    public string Source { get; init; } = string.Empty;
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; init; }
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; init; }
    [JsonPropertyName("deleted")]
    public bool Deleted { get; init; }
    [JsonPropertyName("used")]
    public bool Used { get; init; }
}

public class Status
{
    [JsonPropertyName("verified")]
    public bool Verified { get; init; }
    [JsonPropertyName("sentCount")]
    public int SentCount { get; set; }
}

