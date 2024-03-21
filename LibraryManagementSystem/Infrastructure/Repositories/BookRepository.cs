using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Infrastructure.Constants;
using System.Data;

namespace Infrastructure.Repositories
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        public BookRepository(DatabaseConfiguration databaseConfiguration) : base(databaseConfiguration)
        {
        }

        public void DecreaseBookInventory(string isbn, int decreaseAmount)
        {
            using(var connection = GetSqlConnection())
            {
                var command = new SqlCommand(StoredProcedures.DecreaseBookInventory, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@isbn", SqlDbType.VarChar, 50).Value = isbn;
                command.Parameters.Add("@decreseAmount", SqlDbType.Int).Value = decreaseAmount;
                command.ExecuteNonQuery();
            }
        }

        public void IncreaseBookInventory(string isbn, int increaseAmount)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand(StoredProcedures.IncreaseBookInventory, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@isbn", SqlDbType.VarChar, 50).Value = isbn;
                command.Parameters.Add("@increseAmount", SqlDbType.Int).Value = increaseAmount;
                command.ExecuteNonQuery();
            }
        }

        public void InsertBook(Book book)
        {
            if (BookExists(book.ISBN))
            {
                Console.WriteLine($"Book with ISBN {book.ISBN} already exists.");
            }
            else
            {
                using (var connection = GetSqlConnection())
                {
                    var command = new SqlCommand(StoredProcedures.InsertNewBook, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@isbn", SqlDbType.VarChar, 50).Value = book.ISBN;
                    command.Parameters.Add("@bookTitle", SqlDbType.VarChar, 50).Value = book.BookTitle;
                    command.Parameters.Add("@bookAuthor", SqlDbType.VarChar, 50).Value = book.BookAuthor;
                    command.Parameters.Add("@bookYear", SqlDbType.Int).Value = book.BookYear;
                    command.Parameters.Add("@bookGenre", SqlDbType.VarChar, 50).Value = book.BookGenre;
                    command.Parameters.Add("@bookPagesCount", SqlDbType.Int).Value = book.BookPagesCount;
                    command.Parameters.Add("@bookInventoryCount", SqlDbType.Int).Value = book.BookInventoryCount;
                    command.Parameters.Add("@bookIsAvailable", SqlDbType.Bit).Value = book.BookIsAvailabe;
                    command.ExecuteNonQuery();

                }
            }
        }

        public bool BookExists(string isbn)
        {
            using(var connection = GetSqlConnection())
            {
                var command = new SqlCommand(StoredProcedures.CheckIfBookExists, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@isbn", SqlDbType.VarChar, 50).Value = isbn;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        var count = reader.GetInt32(0);
                        if (count >= 1)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
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
