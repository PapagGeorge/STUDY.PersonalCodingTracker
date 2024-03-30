using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace Domain.Entities
{
    public class Accommodation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AccommodationId { get; set; }
        public string HotelName { get; set; }
        public int StarRating { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PricePerPersonPerDay { get; set; }
        public long DestinationId { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public Destination Destination { get; set; }

    }
}
