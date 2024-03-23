using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IBookRepository
    {
        Book SearchBookByIsbn(string isbn);
        IEnumerable<Book> SearchBookByTitle(string title);
        IEnumerable<Book> ShowRentedBooks();
        IEnumerable<Book> ShowNotRentedBooks();
        IEnumerable<Book> ShowBooksForRental();
        void InsertBook(Book book);
        void IncreaseBookInventory(string isbn, int increaseAmount);
        void DecreaseBookInventory(string isbn, int decreaseAmount);
        void RentBookToUser(string isbn, int userId);
        void ReturnBookFromUser(string isbn, int userId);
        bool BookExists(string isbn);
        bool IsBookInStock (string isbn);
    }
}
