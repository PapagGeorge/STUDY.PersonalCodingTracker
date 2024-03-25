using LibraryApplication.Constants;
using LibraryApplication.Interfaces;
using Domain.Entities;

namespace LibraryApplication.Services
{
    public class Application : IApplication
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;


        public Application(IBookRepository bookRepository, IUserRepository userRepository)
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
        }
        public void SearchBook(string userSearchText)
        {
            try
            {
                var book = new Book();
                var books = new List<Book>();
                book = _bookRepository.SearchBookByIsbn(userSearchText);
                if (book != null)
                {
                    Console.WriteLine($"Your Book with ISBN: {userSearchText}");
                    Console.WriteLine($"Book Title: {book.BookTitle}");
                    Console.WriteLine($"Year: {book.BookYear}");
                    Console.WriteLine($"Author: {book.BookAuthor}");
                    Console.WriteLine($"Genre: {book.BookGenre}");
                    Console.WriteLine($"Pages: {book.BookPagesCount}");
                    Console.WriteLine($"Items Available: {book.BookItemsInStock}");
                }
                else
                {
                    books = (List<Book>)_bookRepository.SearchBookByTitle(userSearchText);

                    if (books != null)
                    {
                        foreach (var singleBook in books)
                        {
                            Console.WriteLine("-----------------------------");
                            Console.WriteLine($"ISBN: {singleBook.ISBN}");
                            Console.WriteLine($"Book Title: {singleBook.BookTitle}");
                            Console.WriteLine($"Year: {singleBook.BookYear}");
                            Console.WriteLine($"Author: {singleBook.BookAuthor}");
                            Console.WriteLine($"Genre: {singleBook.BookGenre}");
                            Console.WriteLine($"Pages: {singleBook.BookPagesCount}");
                            Console.WriteLine($"Items Available: {singleBook.BookItemsInStock}");
                            Console.WriteLine("-----------------------------\n\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNo results were found.");
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured. {ex.Message}");
            }
        }

        public void AvailableBooks()
        {
            try
            {
                var books = new List<Book>();
                books = (List<Book>)_bookRepository.ShowBooksForRental();
                if (books != null)
                {
                    foreach (var singleBook in books)
                    {
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine($"ISBN: {singleBook.ISBN}");
                        Console.WriteLine($"Book Title: {singleBook.BookTitle}");
                        Console.WriteLine($"Year: {singleBook.BookYear}");
                        Console.WriteLine($"Author: {singleBook.BookAuthor}");
                        Console.WriteLine($"Genre: {singleBook.BookGenre}");
                        Console.WriteLine($"Pages: {singleBook.BookPagesCount}");
                        Console.WriteLine($"Items Available: {singleBook.BookItemsInStock}");
                        Console.WriteLine("-----------------------------\n\n");
                    }
                }
                else
                {
                    Console.WriteLine("\nThere are no available books at the moment.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured. {ex.Message}");
            }
        }

        public void AllBooks()
        {
            try
            {
                var books = new List<Book>();
                books = (List<Book>)_bookRepository.ShowAllBooks();
                if (books != null)
                {
                    foreach (var singleBook in books)
                    {
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine($"ISBN: {singleBook.ISBN}");
                        Console.WriteLine($"Book Title: {singleBook.BookTitle}");
                        Console.WriteLine($"Year: {singleBook.BookYear}");
                        Console.WriteLine($"Author: {singleBook.BookAuthor}");
                        Console.WriteLine($"Genre: {singleBook.BookGenre}");
                        Console.WriteLine($"Pages: {singleBook.BookPagesCount}");
                        Console.WriteLine($"Items Available: {singleBook.BookItemsInStock}");
                        Console.WriteLine("-----------------------------\n\n");
                    }
                }
                else
                {
                    Console.WriteLine("\nThe list of books cannot be loaded at the moment.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured. {ex.Message}");
            }
        }

        public void RentBook(string userIsbn, int userId)
        {
            try
            {
                
                if (!_userRepository.UserIdExists(userId))
                {
                    Console.WriteLine("MyLibrary Id you entered does not exist.");
                    return;
                    
                }
                int userBooksAmountRentedBeforeRental = _userRepository.NumberOfBooksRentedByUser(userId);
                
                if (userBooksAmountRentedBeforeRental >= UserRentedBooksLimit.maxBooksRentedByUser)
                {
                    Console.WriteLine($"You have already rented {userBooksAmountRentedBeforeRental} Books.");
                    Console.WriteLine($"Maximum number of Books you can rent is {UserRentedBooksLimit.maxBooksRentedByUser}");
                    Console.WriteLine("Please consider returning a Book in order to rent another one.");
                    return;
                }

                if (!_bookRepository.BookExists(userIsbn))
                {
                    Console.WriteLine("The Book ISBN you entered does not exist.");
                    return;
                    
                }

                if (!_bookRepository.IsBookInStock(userIsbn))
                {
                    Console.WriteLine($"Unfortunatelly no copy of book with ISBN {userIsbn} is available at the moment.");
                    return;
                }

                if (_bookRepository.UserHasRentedBookIsbn(userId, userIsbn))
                {
                    Console.WriteLine($"It seems you have already rented a copy of book with ISBN: {userIsbn}. Please " +
                        $"consider renting another title.");
                    return;
                }
                    

                if(_userRepository.UserIdExists(userId) 
                    && userBooksAmountRentedBeforeRental < UserRentedBooksLimit.maxBooksRentedByUser
                    && _bookRepository.BookExists(userIsbn) && _bookRepository.IsBookInStock(userIsbn) 
                    && !_bookRepository.UserHasRentedBookIsbn(userId, userIsbn))
                {
                    _bookRepository.RentBookToUser(userIsbn, userId);
                    int userBooksAmountRentedAfterRental = _userRepository.NumberOfBooksRentedByUser(userId);
                    if (userBooksAmountRentedAfterRental > userBooksAmountRentedBeforeRental)
                    {
     
                        Console.WriteLine($"\n\nYou have rented Book with ISBN: {userIsbn}");
                        Console.WriteLine($"Maximum number of Books you can rent is {UserRentedBooksLimit.maxBooksRentedByUser}");
                        Console.WriteLine($"You can now rent " +
                            $"{UserRentedBooksLimit.maxBooksRentedByUser - userBooksAmountRentedAfterRental} more Book(s)");
                    }
                    else
                    {
                        
                        Console.WriteLine("Failed to rent the book. Please try again later.");
                    }


                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured. {ex.Message}");
            }

        }

        public void ReturnBook(string userIsbn, int userId)
        {
            try
            {

                if (!_userRepository.UserIdExists(userId))
                {
                    Console.WriteLine("MyLibrary Id you entered does not exist.");
                    return;
                }
                int userBooksAmountRentedBeforeReturn = _userRepository.NumberOfBooksRentedByUser(userId);

                if (!_bookRepository.BookExists(userIsbn))
                {
                    Console.WriteLine("The Book ISBN you entered does not exist.");
                    return;
                }

                if (!_bookRepository.UserHasRentedBookIsbn(userId, userIsbn))
                {
                    Console.WriteLine($"It seems that book with ISBN: {userIsbn} is not rented by you. " +
                        $"Did you type the correct ISBN?");
                }

                if (_userRepository.UserIdExists(userId) && _bookRepository.BookExists(userIsbn)
                    && _bookRepository.UserHasRentedBookIsbn(userId, userIsbn))
                {
                    _bookRepository.ReturnBookFromUser(userIsbn, userId);
                    int userBooksAmountRentedAfterReturn = _userRepository.NumberOfBooksRentedByUser(userId);

                    if (userBooksAmountRentedAfterReturn < userBooksAmountRentedBeforeReturn)
                    {
                        Console.WriteLine($"\n\nYou have returned Book with ISBN: {userIsbn}");
                        Console.WriteLine($"You owe {userBooksAmountRentedAfterReturn} more books.");
                        Console.WriteLine($"Maximum number of Books you can rent is {UserRentedBooksLimit.maxBooksRentedByUser}");
                        Console.WriteLine($"You can now rent " +
                            $"{UserRentedBooksLimit.maxBooksRentedByUser - userBooksAmountRentedAfterReturn} more Book(s)");
                    }
                    else
                    {
                        Console.WriteLine("Failed to return the book. Please try again later.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured. {ex.Message}");
            }
        }
    }
}
