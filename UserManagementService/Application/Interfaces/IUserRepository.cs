using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User> GetUser(Guid userId);
        Task UpdateUser(Guid userId, User user);
    }
}
