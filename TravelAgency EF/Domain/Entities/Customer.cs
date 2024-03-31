using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }
        public int Age { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
