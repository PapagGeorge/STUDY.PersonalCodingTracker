namespace Domain.Models
{
    public class Source
    {
        /*public string Id { get; set; } */// Primary key
        
        public string Id { get; set; } // This can be nullable, corresponding to the `id` field in the API
        public string Name { get; set; }
    }
}
