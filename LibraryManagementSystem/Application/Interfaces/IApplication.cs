using Domain.Entities;

namespace LibraryApplication.Interfaces
{
    public interface IApplication
    {
        void SearchBook(string userSearchText);
        void AvailableBooks();
        void AllBooks();
        void RentBook(string userIsbn, int userId);
        void ReturnBook(string userIsbn, int userId);
        void RegisterUser(User user);
        void DeleteUser(int userId);
        void GetAllUsers();
        void SearchUser(string userSearch);
        void IncreaseBookCopies(string isbn, int increaseAmount);
    }
}
