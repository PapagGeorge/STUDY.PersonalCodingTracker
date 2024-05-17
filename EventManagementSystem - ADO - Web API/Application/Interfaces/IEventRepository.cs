using Domain.Entities;

namespace Application.Interfaces
{
    public interface IEventRepository
    {
        void AddNewEvent(Event newEvent);
    }
}
