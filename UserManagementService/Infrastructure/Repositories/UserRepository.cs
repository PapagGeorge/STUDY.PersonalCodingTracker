using Application.Interfaces;
using Domain;
using Domain.Models;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(Guid userId, User user)
        {
            throw new NotImplementedException();
        }
    }
}
