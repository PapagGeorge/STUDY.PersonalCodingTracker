using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Destination
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DestinationId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
