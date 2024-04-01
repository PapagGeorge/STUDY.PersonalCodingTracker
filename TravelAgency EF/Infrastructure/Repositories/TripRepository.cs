using Domain.Entities;
using Application.Interfaces;

namespace Infrastructure.Repositories
{
    public class TripRepository : ITripRepository
    {
        public IEnumerable<Accommodation> AccommodationByDestination(long destinationId)
        {
            throw new NotImplementedException();
        }

        public void BookAccommodation(long customerId, long AccommodationId)
        {
            throw new NotImplementedException();
        }

        public void BookService(long customerId, long ServiceId)
        {
            throw new NotImplementedException();
        }

        public void BookTransportation(long customerId, long transportationId)
        {
            throw new NotImplementedException();
        }

        public bool isAccommodationAvailable()
        {
            throw new NotImplementedException();
        }

        public bool isServiceAvailable()
        {
            throw new NotImplementedException();
        }

        public bool isTransportationAvailable()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Service> ServicesByDestination(long destinationId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Destination> ShowAllDestinations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transportation> TransporationByDestination(long destinationId)
        {
            throw new NotImplementedException();
        }
    }
}
