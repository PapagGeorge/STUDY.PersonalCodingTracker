using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }
        [AllowNull]
        public DateTime RentDate { get; set; }
        [AllowNull]
        public DateTime ReturnDate { get; set; }
    }
}
