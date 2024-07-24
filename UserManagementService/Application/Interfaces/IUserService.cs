using Domain.Models;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(User user);
        Task<User> GetUserAsync(Guid userId);
        Task UpdateUserAsync(Guid userId, User user);
    }
}
