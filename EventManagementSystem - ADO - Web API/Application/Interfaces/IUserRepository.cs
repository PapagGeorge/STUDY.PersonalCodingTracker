using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
    }
}
