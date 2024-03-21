using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        User SearchUserById(string id);
        IEnumerable<User> SearchUsersByMobilePhone (string mobilePhone);
        void RegisterUser (User user);
        void DeleteUser (int userId);
        bool CanUserRentMoreBooks(int userId);
        

    }
}
