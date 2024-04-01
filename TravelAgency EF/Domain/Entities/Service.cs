using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ServiceId { get; set; }
        public string ServiceName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public long DestinationId { get; set; }
        public int Availability {  get; set; }
        public bool isAvailable { get; set; }
        public Destination Destination { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<PackageService> PackageServices { get; set; }
    }
}
