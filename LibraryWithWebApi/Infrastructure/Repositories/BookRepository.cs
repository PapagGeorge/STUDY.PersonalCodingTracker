using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
            
        }

        public void AddBook(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to add new book. {ex.Message}");
            }
        }

        public bool BookExists(int bookId)
        {
            try
            {
                return _context.Books.Any(book => book.BookId == bookId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to check if book with Id: {bookId} exists. {ex.Message}");
            }
        }

        public bool BookIsOwed(int bookId)
        {
            try
            {
                var book = _context.Books.FirstOrDefault(book => book.BookId == bookId);
                if (book.Inventory == NumberOfBooksAvailable(bookId))
                {
                    return false;

                }
                else return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to check if book with Id: {bookId} is owed. {ex.Message}");
            }
        }

        public void DecreaseBookAvailability(int bookId, int decreaseNumber)
        {
            try
            {
                var book = _context.Books.FirstOrDefault(book => book.BookId == bookId);
                book.RentedCount -= decreaseNumber;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to decrease availability of book with Id: {bookId}. {ex.Message}");
            }
        }

        public IEnumerable<Book> GetAllActiveAvailableBooks()
        {
            try
            {
                var result = _context.Books.Where(book => book.isDeleted == false && book.IsAvailable == true);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to get all books. {ex.Message}");
            }
        }

        public void IncreaseBookAvailability(int bookId, int increaseNumber)
        {
            try
            {
                var book = _context.Books.FirstOrDefault(book => book.BookId == bookId);
                book.RentedCount += increaseNumber;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to increase availability of book with Id: {bookId}. {ex.Message}");
            }
        }

        public bool IsBookAvailable(int bookId)
        {
            try
            {
                var bookToCheck = _context.Books.FirstOrDefault(book => book.BookId == bookId);

                if(bookToCheck.IsAvailable == true && bookToCheck != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to check availability of book with Id: {bookId}.  {ex.Message}");
            }
        }

        public bool isBookDeleted(int bookId)
        {
            try
            {
                var book = _context.Books.FirstOrDefault(book => book.BookId == bookId);

                if(book.isDeleted == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to check if book with Id: {bookId} is deleted.  {ex.Message}");
            }
        }

        public void MakeBookAvailable(int bookId)
        {
            try
            {
                var book = _context.Books.FirstOrDefault(book => book.BookId == bookId);
                book.IsAvailable = true;
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to make book with Id: {bookId} available.  {ex.Message}");
            }
        }

        public void MakeBookUnavailable(int bookId)
        {
            try
            {
                var book = _context.Books.FirstOrDefault(b => b.BookId == bookId);
                book.IsAvailable = false;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to make book with Id: {bookId} unavailable.  {ex.Message}");
            }
        }

        public int NumberOfBooksAvailable(int bookId)
        {
            try
            {
                var book = _context.Books.FirstOrDefault(b => b.BookId == bookId);
                return (book.Inventory - book.RentedCount);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to find how many copies " +
                    $"are available for book with Id: {bookId}.  {ex.Message}");
            }
        }

        public void SoftDeleteBook(int bookId)
        {
            try
            {
                
                var book = _context.Books.FirstOrDefault(book => book.BookId == bookId);
                if (book != null)
                {
                    book.isDeleted = true;
                    _context.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to delete book.  {ex.Message}");
            }
        }
    }
}
