using Application.Interfaces;
using Domain;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUser(Guid userId, User user)
        {
            throw new NotImplementedException();
        }
    }
}
