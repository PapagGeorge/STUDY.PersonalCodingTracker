using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task AddUserAsync(User user)
        {
            try
            {
                // Check if user already exists
                if (await _userRepository.UserExistsAsync(user.Username, user.Email))
                {
                    throw new InvalidOperationException("User with the same username or email already exists.");
                }

                // Add user
                await _userRepository.AddUserAsync(user);
                _logger.LogInformation("User added successfully.");
            }
            catch (ApplicationException ex)
            {
                // Log and rethrow with a user-friendly message
                _logger.LogError(ex, "Failed to add user.");
                throw new ApplicationException("Failed to add user. Please try again later.");
            }
            catch (Exception ex)
            {
                // Log and rethrow with a user-friendly message
                _logger.LogError(ex, "An unexpected error occurred while adding the user.");
                throw new ApplicationException("An unexpected error occurred. Please try again later.");
            }
        }

        public Task<User> GetUserAsync(Guid userId)
        {
            try
            {
                var user = _userRepository.GetUserAsync(userId);

                if (user == null)
                {
                    _logger.LogWarning($"User with ID {userId} not found.");
                    throw new KeyNotFoundException("User not found.");
                }

                _logger.LogInformation($"User with ID {userId} retrieved successfully.");
                return user;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Failed to retrieve user.");
                throw new ApplicationException("User not found. Please check the user ID and try again.");
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Failed to retrieve user.");
                throw new ApplicationException("Failed to retrieve user. Please try again later.");
            }
        }

        public async Task UpdateUserAsync(Guid userId, User user)
        {
            try
            {
                var userExists = await _userRepository.GetUserAsync(userId);
                if (userExists == null)
                {
                    _logger.LogWarning($"User with ID {userId} not found.");
                    throw new ApplicationException("User not found. Cannot update a non-existent user.");
                }

                await _userRepository.UpdateUserAsync(userId, user);
                _logger.LogInformation("User updated successfully.");
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Failed to update user.");
                throw new ApplicationException("Failed to update user. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating the user.");
                throw new ApplicationException("An unexpected error occurred. Please try again later.");
            }
        }
    }
}
