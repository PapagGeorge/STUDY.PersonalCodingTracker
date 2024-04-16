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

        public void RentBook(RentBookRequest rentBookRequest)
        {
            try
            {
                var transactionRecord = new Transaction()
                {
                    MemberId = rentBookRequest.MemberId,
                    BookId = rentBookRequest.BookId,
                    RentDate = DateTime.Now,
                    ReturnDate = null
                };

                _context.Transactions.Add(transactionRecord);
                _context.SaveChanges();
                
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to rent Book " +
                    $"with Id: {rentBookRequest.BookId} to Member with Id: {rentBookRequest.MemberId}. {ex.Message}");
            }
        }

        public void ReturnBook(ReturnBookRequest returnBookRequest)
        {
            try
            {
                var transactionRecord = _context.Transactions.FirstOrDefault(trans => trans.BookId == returnBookRequest.BookId
                && trans.ReturnDate == null);

                transactionRecord.ReturnDate = DateTime.Now;
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to return Book " +
                    $"with Id: {returnBookRequest.BookId} to Member with Id: {returnBookRequest.MemberId}. {ex.Message}");
            }
        }
    }
}
