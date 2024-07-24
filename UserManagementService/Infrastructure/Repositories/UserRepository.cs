using Application.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;
        public UserRepository(UserDbContext context)
        {
            _context = context;
        }
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            var user = await _context.Users.Where(u => u.UserId == userId).FirstOrDefaultAsync();
            return user;
        }

        public async Task UpdateUserAsync(Guid userId, User user)
        {
            var userToUpdate = await _context.Users.Where(user => user.UserId == userId).FirstOrDefaultAsync();

            if (userToUpdate == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            userToUpdate.Username = user.Username;
            userToUpdate.PasswordHash = user.PasswordHash;
            userToUpdate.Email = user.Email;
            userToUpdate.FullName = user.FullName;
            userToUpdate.DateOfBirth = user.DateOfBirth;
            userToUpdate.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(string userName, string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email || u.Username == userName);
        }
    }
}
