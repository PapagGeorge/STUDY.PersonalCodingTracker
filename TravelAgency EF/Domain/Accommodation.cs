using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace Domain
{
    public class Accommodation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AccommodationId { get; set; }
        public string HotelName { get; set; }
        public int StarRating { get; set; }
        public int Duration { get; set; }
        public decimal PricePerPersonPerDay { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
