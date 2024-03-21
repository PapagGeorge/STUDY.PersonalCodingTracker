using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Infrastructure.Constants;
using System.Data;
using System.Transactions;
using Infrastructure.Repositories;

namespace Infrastructure.Repositories
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        private readonly IUserRepository _userRepository;
        public BookRepository(DatabaseConfiguration databaseConfiguration, IUserRepository userRepository) : base(databaseConfiguration)
        {
            _userRepository = userRepository;
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
            if (_userRepository.CanUserRentMoreBooks(userId))
            {
                using(var connection = GetSqlConnection())
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var command1 = new SqlCommand(StoredProcedures.RentBookToUser, connection);
                            command1.CommandType = CommandType.StoredProcedure;
                            command1.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                            command1.Parameters.Add("@ISBN", SqlDbType.Int).Value = isbn;
                            command1.ExecuteNonQuery();

                            var command2 = new SqlCommand(StoredProcedures.SelectUsersRentedMoviesCount, connection);
                            command2.CommandType = CommandType.StoredProcedure;
                            command2.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                            using (var reader = command2.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    var numberOfMoviesRented = reader.GetInt32(0);

                                    if (numberOfMoviesRented >= 2)
                                    {
                                        _userRepository.RemoveUserRentability(userId);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"There is no user with id: {userId}");
                                }
                                
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }             
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
