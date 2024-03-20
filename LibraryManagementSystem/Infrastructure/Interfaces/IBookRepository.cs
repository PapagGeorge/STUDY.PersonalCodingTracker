using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IBookRepository
    {
        Book SearchBookByIsbn(string isbn);
        Book SearchBookByTitle(string title);
        IEnumerable<Book> ShowRentedBooks();
        IEnumerable<Book> ShowBooksForRental();
        void InsertBook();
        void IncreaseBookInventory(string isbn);
        void DecreaseBookInventory(string isbn);
        void RentBookToUser(string isbn, int userId);
        void ReturnBookFromUser(string isbn, int userId);
    }
}
