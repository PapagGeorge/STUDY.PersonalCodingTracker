using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Transportation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TransportationId { get; set; }
        
        public string TransportationMode { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public long DestinationId { get; set; }
        public Destination Destination { get; set; }
        
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<PackageTransportation> PackageTransportation { get; set; }
        [Required]
        public bool IsAvailable { get; set; } = true;
        public int Availability { get; set; }
    }
}
