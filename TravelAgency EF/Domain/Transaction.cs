using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Transaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        public int CustomerId { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public int PackageId { get; set; }
        public int TransportationId { get; set; }
        public int AccommodationId { get; set; }
        public int ServiceId { get; set; }
        public decimal Amount { get; set; }
        public Customer Customer { get; set; }
        public Package Package { get; set; }
        public Transportation Transportation { get; set; }
        public Accommodation Accommodation { get; set; }
        public Service Service { get; set; }


    }
}
