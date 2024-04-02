using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InvoiceId { get; set; }
        public long CustomerId { get; set; }
        public DateTime IssuedDate { get; set; } = DateTime.Now;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Payment> Payments { get; set; }
        
    }
}
