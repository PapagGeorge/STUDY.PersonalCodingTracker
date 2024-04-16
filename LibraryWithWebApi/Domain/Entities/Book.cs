using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        [AllowNull]
        public int Year { get; set; }
        public string Author { get; set; }
        public int Inventory { get; set; }
        public int RentedCount { get; set; }
        public bool IsAvailable { get; set; }
        public bool isDeleted { get; set; } = false;
        //[JsonIgnore]
        //public ICollection<Transaction> Transactions { get; set; }

    }
}
