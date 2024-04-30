using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAttendanceRepository
    {
        IEnumerable<User> AttendedEventsByUser(int userId);
    }
}
