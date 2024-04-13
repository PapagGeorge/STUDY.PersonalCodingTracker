using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBookRepository
    {
        bool BookExists(int bookId);
        void AddBook(Book book);
        void SoftDeleteBook(int bookId);
        IEnumerable<Book> GetAllBooks();
        bool IsBookAvailable(int bookId);

    }
}
