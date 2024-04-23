using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        bool UserExists(int userId);
        User GetUserById(int userId);
        IEnumerable<User> GetAllUsers();
    }
}
