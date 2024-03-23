using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Infrastructure.Constants;
using System.Data;
using System.Transactions;
using Infrastructure.Repositories;
using static System.Reflection.Metadata.BlobBuilder;


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
                throw new Exception($"An error occurred while decreasing book inventory: {ex.Message}");
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
                throw new Exception($"An error occurred while increasing book inventory: {ex.Message}");
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
                    throw new Exception($"An error occurred while trying to insert a new book: {ex.Message}");
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
                        return false;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while trying to check if the book already exists: {ex.Message}");
                
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
            else
            {
                throw new Exception("An error occured. Please check if your userId and Book ISBN are valid.");
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
                                
                                return false;
                            }
                        }
                        else
                        {
                            throw new Exception($"Book with ISBN {isbn} was not found.");
                        }
                        
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
                                    throw new Exception($"There is no user with id: {userId}");
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
            using (var connection = GetSqlConnection())
            {
                try
                {
                    var book = new Book();
                    var command = new SqlCommand(StoredProcedures.SearchBookByIsbn, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Isbn", SqlDbType.VarChar, 50).Value = isbn;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            book.ISBN = reader["ISBN"].ToString() ?? string.Empty;
                            book.BookTitle = reader["Book_Title"].ToString() ?? string.Empty;
                            book.BookAuthor = reader["Book_Author"].ToString() ?? string.Empty;
                            book.BookYear = Convert.ToInt32(reader["Book_Year"]);
                            book.BookGenre = reader["Book_Genre"].ToString() ?? string.Empty;
                            book.BookPagesCount = Convert.ToInt32(reader["Book_Pages_Count"]);
                            book.BookItemsInStock = Convert.ToInt32(reader["Book_Items_In_Stock"]);
                            book.BookIsAvailabe = reader.GetBoolean(reader.GetOrdinal("Book_Is_Available"));

                        }
                        else
                        {
                            return book;
                        }
                    }
                    return book;


                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occured while trying to search for book with ISBN = {isbn}. {ex.Message}");
                }
            }
                     
        }

        public IEnumerable<Book> SearchBookByTitle(string title)
        {
            var books = new List<Book>();
            using (var connection = GetSqlConnection())
            {
                try
                {
                    var command = new SqlCommand(StoredProcedures.SearchBookByTitle, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Title", SqlDbType.VarChar, 50).Value = title;

                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var book = new Book();
                            book.ISBN = reader["ISBN"].ToString() ?? string.Empty;
                            book.BookTitle = reader["Book_Title"].ToString() ?? string.Empty;
                            book.BookAuthor = reader["Book_Author"].ToString() ?? string.Empty;
                            book.BookYear = Convert.ToInt32(reader["Book_Year"]);
                            book.BookGenre = reader["Book_Genre"].ToString() ?? string.Empty;
                            book.BookPagesCount = Convert.ToInt32(reader["Book_Pages_Count"]);
                            book.BookItemsInStock = Convert.ToInt32(reader["Book_Items_In_Stock"]);
                            book.BookIsAvailabe = reader.GetBoolean(reader.GetOrdinal("Book_Is_Available"));

                            books.Add(book);
                        }                      
                    }             
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occured while trying to search for book with Title = {title}. {ex.Message}");
                }
                return books;
            }
        }

        public IEnumerable<Book> ShowBooksForRental()
        {
            var books = new List<Book>();
            using (var connection = GetSqlConnection())
            {
                try
                {
                    var command = new SqlCommand(StoredProcedures.ShowBooksForRental, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var book = new Book();
                            book.ISBN = reader["ISBN"].ToString() ?? string.Empty;
                            book.BookTitle = reader["Book_Title"].ToString() ?? string.Empty;
                            book.BookAuthor = reader["Book_Author"].ToString() ?? string.Empty;
                            book.BookYear = Convert.ToInt32(reader["Book_Year"]);
                            book.BookGenre = reader["Book_Genre"].ToString() ?? string.Empty;
                            book.BookPagesCount = Convert.ToInt32(reader["Book_Pages_Count"]);
                            book.BookItemsInStock = Convert.ToInt32(reader["Book_Items_In_Stock"]);
                            book.BookIsAvailabe = reader.GetBoolean(reader.GetOrdinal("Book_Is_Available"));

                            books.Add(book);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occured while trying to search for available books. {ex.Message}");
                }
                return books;
            }
        }

        public IEnumerable<Book> ShowRentedBooks()
        {
            var books = new List<Book>();
            using (var connection = GetSqlConnection())
            {
                try
                {
                    var command = new SqlCommand(StoredProcedures.ShowRentedBooks, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var book = new Book();
                            book.ISBN = reader["ISBN"].ToString() ?? string.Empty;
                            book.BookTitle = reader["Book_Title"].ToString() ?? string.Empty;
                            book.BookAuthor = reader["Book_Author"].ToString() ?? string.Empty;
                            book.BookYear = Convert.ToInt32(reader["Book_Year"]);
                            book.BookGenre = reader["Book_Genre"].ToString() ?? string.Empty;
                            book.BookPagesCount = Convert.ToInt32(reader["Book_Pages_Count"]);
                            book.BookItemsInStock = Convert.ToInt32(reader["Book_Items_In_Stock"]);
                            book.BookIsAvailabe = reader.GetBoolean(reader.GetOrdinal("Book_Is_Available"));

                            books.Add(book);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occured while trying to find all rented books. {ex.Message}");
                }
                return books;
            }
        }

        public IEnumerable<Book> ShowNotRentedBooks()
        {
            var books = new List<Book>();
            using (var connection = GetSqlConnection())
            {
                try
                {
                    var command = new SqlCommand(StoredProcedures.ShowNotRentedBooks, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var book = new Book();
                            book.ISBN = reader["ISBN"].ToString() ?? string.Empty;
                            book.BookTitle = reader["Book_Title"].ToString() ?? string.Empty;
                            book.BookAuthor = reader["Book_Author"].ToString() ?? string.Empty;
                            book.BookYear = Convert.ToInt32(reader["Book_Year"]);
                            book.BookGenre = reader["Book_Genre"].ToString() ?? string.Empty;
                            book.BookPagesCount = Convert.ToInt32(reader["Book_Pages_Count"]);
                            book.BookItemsInStock = Convert.ToInt32(reader["Book_Items_In_Stock"]);
                            book.BookIsAvailabe = reader.GetBoolean(reader.GetOrdinal("Book_Is_Available"));

                            books.Add(book);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occured while trying to find all not rented books. {ex.Message}");
                }
                return books;
            }
        }
    }
}
