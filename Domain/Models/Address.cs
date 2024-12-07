using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain;
public class Address
{
    [Key]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("street")]
    public string Street { get; set; }
    
    [JsonPropertyName("city")]
    public string City { get; set; }
    
    [JsonPropertyName("state")]
    public string State { get; set; }
    
    [JsonPropertyName("postalCode")]
    public string PostalCode { get; set; }
    
    [JsonPropertyName("country")]
    public string Country { get; set; }
    
    public List<Order> Orders { get; set; } = new List<Order>();
}