using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BetDbContext _context;

        public UserRepository(BetDbContext context)
        {
            _context = context;
        }
        public void CreateUser(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new Exception("User object is null");
                }
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to add new user. {ex.Message}");
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                return _context.Users;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to get all existing users. {ex.Message}");
            }
        }

        public User GetUserById(int userId)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(user => user.UserId == userId);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to get user with Id: {userId}. {ex.Message}");
            }
        }

        public bool UserExists(int userId)
        {
            try
            {
                return _context.Users.Any(user => user.UserId == userId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to find if user with Id: {userId} exists. {ex.Message}");
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                return _context.Users;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to get all existing users. {ex.Message}");
            }
        }

        public User GetUserById(int userId)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(user => user.UserId == userId);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to get user with Id: {userId}. {ex.Message}");
            }
        }
    }
}
