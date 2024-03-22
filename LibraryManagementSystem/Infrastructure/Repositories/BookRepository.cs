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
            try
            {
                using (var connection = GetSqlConnection())
                {
                    var command = new SqlCommand(StoredProcedures.DecreaseBookInventory, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@isbn", SqlDbType.VarChar, 50).Value = isbn;
                    command.Parameters.Add("@decreseAmount", SqlDbType.Int).Value = decreaseAmount;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while decreasing book inventory: {ex.Message}");
            }
        }

        public void IncreaseBookInventory(string isbn, int increaseAmount)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while increasing book inventory: {ex.Message}");
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
                try
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
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while trying to insert a new book: {ex.Message}");
                }
            }
        }

        public bool BookExists(string isbn)
        {
            try
            {
                using (var connection = GetSqlConnection())
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
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while trying to check if the book already exists: {ex.Message}");
                return false;
            }
        }

        public void RentBookToUser(string isbn, int userId)
        {
            if (_userRepository.UserIdExists(userId) && _userRepository.CanUserRentMoreBooks(userId)
                && BookExists(isbn) && IsBookInStock(isbn))
            {
                using(var connection = GetSqlConnection())
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var command1 = new SqlCommand(StoredProcedures.RentBookToUser, connection, transaction);
                            command1.CommandType = CommandType.StoredProcedure;
                            command1.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                            command1.Parameters.Add("@ISBN", SqlDbType.VarChar, 50).Value = isbn;
                            command1.ExecuteNonQuery();

                            var command2 = new SqlCommand(StoredProcedures.SelectUsersRentedMoviesCount, connection, transaction);
                            command2.CommandType = CommandType.StoredProcedure;
                            command2.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                            using (var reader = command2.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    var numberOfMoviesRented = reader.GetInt32(0);

                                    if (numberOfMoviesRented >= Limits.MaxBooksPerUser)
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
                            Console.WriteLine($"An error occured while trying to rent book with " +
                            $"ISBN: {isbn} to user with Id: {userId}. {ex.Message}");
                            transaction.Rollback();
                        }
                    }
                }
            }             
        }

        public bool IsBookInStock(string isbn)
        {
            using(var connection = GetSqlConnection())
            {
                try
                {
                    var command = new SqlCommand(StoredProcedures.NumberOfCopiesInStock, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Isbn", SqlDbType.VarChar, 50).Value = isbn;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            var numberOfBooksInStock = reader.GetInt32(0);
                            if (numberOfBooksInStock > 0)
                            {
                                return true;
                            }
                            else
                            {
                                Console.WriteLine($"Book with ISBN {isbn} is out of stock.");
                                return false;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Book with ISBN {isbn} does not exist.");
                        }
                        return false;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"An error occured: {ex.Message}");
                    return false;
                }
            }
        }

        public void ReturnBookFromUser(string isbn, int userId)
        {
                using (var connection = GetSqlConnection())
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var command1 = new SqlCommand(StoredProcedures.ReturnBookFromUser, connection, transaction);
                            command1.CommandType = CommandType.StoredProcedure;
                            command1.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                            command1.Parameters.Add("@ISBN", SqlDbType.VarChar, 50).Value = isbn;
                            command1.ExecuteNonQuery();

                            var command2 = new SqlCommand(StoredProcedures.SelectUsersRentedMoviesCount, connection, transaction);
                            command2.CommandType = CommandType.StoredProcedure;
                            command2.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                            using (var reader = command2.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    var numberOfMoviesRented = reader.GetInt32(0);

                                    if (numberOfMoviesRented <= Limits.MaxBooksPerUser)
                                    {
                                        _userRepository.RestoreUserRentability(userId);
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
                        Console.WriteLine($"An error occured while trying to return book with " +
                            $"ISBN: {isbn} from user with Id: {userId}. {ex.Message}");
                        transaction.Rollback();
                        }
                    }
                }
            
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
