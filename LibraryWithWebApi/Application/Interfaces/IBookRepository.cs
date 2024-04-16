using Domain.Entities;
using System.Net;

namespace Application.Interfaces
{
    public interface IBookRepository
    {
        bool BookExists(int bookId);
        void AddBook(Book book);
        void SoftDeleteBook(int bookId);
        IEnumerable<Book> GetAllBooks();
        bool IsBookAvailable(int bookId);
        IEnumerable<Book> GetAllActiveAvailableBooks();
        void MakeBookAvailable(int bookId);
        int NumberOfBooksAvailable(int bookId);
        void IncreaseBookAvailability(int bookId, int increaseNumber);
        void MakeBookUnavailable(int bookId);
        void DecreaseBookAvailability(int bookId, int decreaseNumber);
        bool isBookDeleted(int bookId);
        bool BookIsOwed(int bookId);

    }
}
