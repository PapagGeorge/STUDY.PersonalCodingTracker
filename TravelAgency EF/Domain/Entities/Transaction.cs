using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TransactionId { get; set; }
        public long CustomerId { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public long PackageId { get; set; }
        public long TransportationId { get; set; }
        public long AccommodationId { get; set; }
        public long ServiceId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public Customer Customer { get; set; }
        public Package Package { get; set; }
        public Transportation Transportation { get; set; }
        public Accommodation Accommodation { get; set; }
        public Service Service { get; set; }


    }
}
