using Domain.Models;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User> GetUserAsync(Guid userId);
        Task UpdateUserAsync(Guid userId, User user);
        Task<bool> UserExistsAsync(string userName, string email);
    }
}
