using Domain.Models;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User> GetUser(Guid userId);
        Task UpdateUser(Guid userId, User user);
    }
}
