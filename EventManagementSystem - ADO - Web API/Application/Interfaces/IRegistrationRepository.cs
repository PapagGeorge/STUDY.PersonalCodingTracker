using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRegistrationRepository
    {
        IEnumerable<Registration> RegistrationsPerUser (int userId);
        IEnumerable<User> RegisteredUsersPerEvent (int eventId);
        void BulkInsertRegistrations(IEnumerable<Registration> registrations);
    }
}
