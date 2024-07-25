using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IDistributedCache _distributedCache;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger, IDistributedCache distributedCache)
        {
            _userRepository = userRepository;
            _logger = logger;
            _distributedCache = distributedCache;
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

        public async Task<User> GetUserAsync(Guid userId)
        {
            try
            {
                var cacheKey = userId.ToString();
                // Attempt to retrieve user from cache
                var user = await _distributedCache.GetRecordAsync<User>(cacheKey, GetJsonSerializerOptions());

                if(user == null)
                {
                    // User not in cache, fetch from repository
                    user = await _userRepository.GetUserAsync(userId);

                    if (user == null)
                    {
                        // User not found in repository, create a default user or handle accordingly
                        user = CreateDefaultUser();
                    }
                    else
                    {
                        // Add user to cache
                        await _distributedCache.SetRecordAsync(cacheKey, user);
                    }
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving user with ID {UserId}.", userId);
                throw new ApplicationException("An unexpected error occurred. Please try again later.");
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
        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        private User CreateDefaultUser()
        {
            return new User
            {
                Username = "Error",
                Email = "Error",
                FullName = "Error",
                DateOfBirth = DateTime.MinValue,
                CreatedAt = DateTime.MinValue
            };
        }
    }
}
