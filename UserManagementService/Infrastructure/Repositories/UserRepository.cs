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
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("An error occurred while adding the user.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred.", ex);
            }
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            try
            {
                var user = await _context.Users.Where(u => u.UserId == userId).FirstOrDefaultAsync();
                if (user == null)
                {
                    throw new KeyNotFoundException("User not found.");
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while retrieving the user.", ex);
            }
        }
            
        public async Task UpdateUserAsync(Guid userId, User user)
        {
            try
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
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("An error occurred while updating the user.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred.", ex);
            }
        }

        public async Task<bool> UserExistsAsync(string userName, string email)
        {
            try
            {
                return await _context.Users.AnyAsync(u => u.Email == email || u.Username == userName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while checking user existence.", ex);
            }
        }
    }
}
