using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }
        //[JsonIgnore]
        //public Book Book { get; set; }
        //[JsonIgnore]
        //public Member Member { get; set; }
        [AllowNull]
        public DateTime? RentDate { get; set; }
        [AllowNull]
        public DateTime? ReturnDate { get; set; }
    }
}
