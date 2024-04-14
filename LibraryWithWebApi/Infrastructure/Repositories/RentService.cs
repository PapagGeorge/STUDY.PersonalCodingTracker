using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class RentService : IRentService
    {
        private readonly LibraryDbContext _context;

        public RentService(LibraryDbContext context)
        {
            _context = context;
        }

        public void RentBook(int memberId, int bookId)
        {
            try
            {
                var transactionRecord = new Transaction()
                {
                    MemberId = memberId,
                    BookId = bookId,
                    RentDate = DateTime.Now
                };

                _context.Transactions.Add(transactionRecord);
                _context.SaveChanges();
                
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to rent Book " +
                    $"with Id: {bookId} to Member with Id: {memberId}. {ex.Message}");
            }
        }

        public void ReturnBook(int memberId, int bookId)
        {
            try
            {
                var transactionRecord = new Transaction()
                {
                    MemberId = memberId,
                    BookId = bookId,
                    ReturnDate = DateTime.Now
                };

                _context.Transactions.Add(transactionRecord);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to return Book " +
                    $"with Id: {bookId} to Member with Id: {memberId}. {ex.Message}");
            }
        }
    }
}
