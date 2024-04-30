using Domain.Entities;

namespace Application.Interfaces
{
    public interface IEventRepository
    {
        IEnumerable<Event> EventsInDateRange (DateTime start, DateTime end);
    }
}
