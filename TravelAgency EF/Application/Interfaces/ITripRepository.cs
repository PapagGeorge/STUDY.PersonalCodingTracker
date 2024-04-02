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
        bool IsServiceAvailable(long serviceId);
        bool IsTransportationAvailable(long TransportationId);
        bool IsAccommodationAvailable(long accommodationId);
        bool ServiceExists(long serviceId);
        bool AccommodationExists(long accommodationId);
        bool TransportationExists(long transportationId);

    }
}
