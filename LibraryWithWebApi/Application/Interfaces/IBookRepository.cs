using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBookRepository
    {
        bool BookExists(int bookId);
        void AddBook(Book book);
        void SoftDeleteBook(int bookId);
        IEnumerable<Book> GetAllActiveAvailableBooks();
        bool IsBookAvailable(int bookId);
        void MakeBookAvailable(int bookId);
        void MakeBookUnavailable(int bookId);
        int NumberOfBooksAvailable(int bookId);
        bool BookIsOwed (int bookId);
        void IncreaseBookAvailability(int bookId, int increaseNumber);
        void DecreaseBookAvailability(int bookId, int decreaseNumber);
        bool isBookDeleted(int bookId);
        
    }
}
