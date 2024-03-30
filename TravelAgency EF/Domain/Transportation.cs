using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Transportation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransportationId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string TransportationMode { get; set; }
        public decimal Price { get; set; }
        public ICollection<Transaction> Transaction { get; set; }
    }
}
