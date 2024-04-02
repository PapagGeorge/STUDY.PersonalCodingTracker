using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITripRepository
    {
        void BookTransportation(long customerId, long transportationId);
        void BookAccommodation(long customerId, long AccommodationId, int daysOfVisit);
        void BookService(long customerId, long ServiceId);
        IEnumerable<Destination> ShowAllDestinations();
        IEnumerable<Accommodation> AccommodationByDestination(long destinationId);
        IEnumerable<Transportation> TransporationByDestination(long destinationId);
        IEnumerable<Service> ServicesByDestination(long destinationId);
        bool isServiceAvailable(long serviceId);
        bool isTransportationAvailable(long TransportationId);
        bool isAccommodationAvailable(long accommodationId);
        bool serviceExists(long serviceId);
        bool accommodationExists(long accommodationId);
        bool transportationExists(long transportationId);

    }
}
