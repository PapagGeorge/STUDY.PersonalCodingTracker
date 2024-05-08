using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
        void AddUser(User newUser);
        void BulkInsertUsers(IEnumerable<User> users);
    }
}
