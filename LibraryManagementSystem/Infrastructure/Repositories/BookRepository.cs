using Domain.Entities;
using LibraryApplication.Interfaces;
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

            using (var connection = GetSqlConnection())
            {

                try
                {
                    var command1 = new SqlCommand(StoredProcedures.RentBookToUser, connection);
                    command1.CommandType = CommandType.StoredProcedure;
                    command1.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    command1.Parameters.Add("@ISBN", SqlDbType.VarChar, 50).Value = isbn;
                    command1.ExecuteNonQuery();
                }


                catch (Exception ex)
                {
                    Console.WriteLine($"An error occured while trying to rent book with " +
                    $"ISBN: {isbn} to user with Id: {userId}. {ex.Message}");

                }

            }

        }

        public bool IsBookInStock(string isbn)
        {
            using (var connection = GetSqlConnection())
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
                catch (Exception ex)
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
                
                    try
                    {
                        var command1 = new SqlCommand(StoredProcedures.ReturnBookFromUser, connection);
                        command1.CommandType = CommandType.StoredProcedure;
                        command1.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        command1.Parameters.Add("@ISBN", SqlDbType.VarChar, 50).Value = isbn;
                        command1.ExecuteNonQuery();

                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occured while trying to return book with " +
                            $"ISBN: {isbn} from user with Id: {userId}. {ex.Message}");
                        
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
                            return null;
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
                        if (reader.HasRows)
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
                        else
                        {
                            return null;
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
                        if (reader.HasRows)
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
                        else
                        {
                            return null;
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
                        if (reader.HasRows)
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
                        else
                        {
                            return null;
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

        public IEnumerable <Book> ShowAllBooks()
        {
            var books = new List<Book>();
            using (var connection = GetSqlConnection())
            {
                try
                {
                    var command = new SqlCommand(StoredProcedures.ShowAllBooks, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
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
                        else
                        {
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occured while trying to find all books. {ex.Message}");
                }
                return books;
            }
        }

        bool UserHasRentedBookIsbn(int userId, string isbn)
        {
            try
            {
                using (var connection = GetSqlConnection())
                {
                    var command = new SqlCommand(StoredProcedures.CheckUserHasRentedBook, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Isbn", SqlDbType.VarChar, 50).Value = isbn;
                    command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                    using (var reader = command.ExecuteReader())
                    {
                        int copiesCount = reader.GetInt32(0);

                        if (copiesCount > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occured while trying to check if user with Id: {userId} has already" +
                    $" rented book with ISBN: {isbn}. {ex.Message}");
            }
        }
    }
}
