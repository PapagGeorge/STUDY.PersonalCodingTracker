using Domain.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        public BookRepository(DatabaseConfiguration databaseConfiguration) : base(databaseConfiguration)
        {
        }

        public void DecreaseBookInventory(string isbn)
        {
            throw new NotImplementedException();
        }

        public void IncreaseBookInventory(string isbn)
        {
            throw new NotImplementedException();
        }

        public void InsertBook()
        {
            throw new NotImplementedException();
        }

        public void RentBookToUser(string isbn, int userId)
        {
            throw new NotImplementedException();
        }

        public void ReturnBookFromUser(string isbn, int userId)
        {
            throw new NotImplementedException();
        }

        public Book SearchBookByIsbn(string isbn)
        {
            throw new NotImplementedException();
        }

        public Book SearchBookByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> ShowBooksForRental()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> ShowRentedBooks()
        {
            throw new NotImplementedException();
        }
    }
}
