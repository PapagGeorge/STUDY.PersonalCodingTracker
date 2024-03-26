using Domain.Entities;

namespace LibraryApplication.Interfaces
{
    public interface IUserRepository
    {
        User SearchUserById(int id);
        IEnumerable<User> SearchUsersByMobilePhone(string mobilePhone);
        void RegisterUser(User user);
        void DeleteUser(int userId);  
        void RemoveUserRentability(int userId);
        void RestoreUserRentability(int userId);
        bool UserIdExists(int userId);
        int NumberOfBooksRentedByUser(int userId);
        int CountUsers();
        bool UserHasRentedBookIsbn(int userId, string isbn);
        IEnumerable<User> UserList();


    }
}
