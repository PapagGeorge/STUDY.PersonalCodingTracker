using Domain.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(DatabaseConfiguration databaseConfiguration) : base(databaseConfiguration)
        {
        }

        public bool CanUserRentMoreBooks(int userId)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public void RegisterUser(User user)
        {
            throw new NotImplementedException();
        }

        public User SearchUserById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> SearchUsersByMobilePhone(string mobilePhone)
        {
            throw new NotImplementedException();
        }
    }
}
