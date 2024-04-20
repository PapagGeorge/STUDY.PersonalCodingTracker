using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        void NewUser(User user);
    }
}
