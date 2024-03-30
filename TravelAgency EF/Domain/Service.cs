using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Service
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
