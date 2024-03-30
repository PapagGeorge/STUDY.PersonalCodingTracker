using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Invoice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime IssuedDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Payment> Payments { get; set; }

    }
}
