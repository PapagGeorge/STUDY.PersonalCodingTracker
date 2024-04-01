using Domain.Entities;
using Application.Interfaces;
using Domain;

namespace Infrastructure.Repositories
{
    public class TripRepository : ITripRepository
    {

        private readonly TravelAgencyDbContext context;

        public TripRepository(TravelAgencyDbContext _context)
        {
            context = _context;
        }

        public IEnumerable<Accommodation> AccommodationByDestination(long destinationId)
        {
            try
            {
                var accommodationOptions = context.Accommodation.Where(accom => accom.DestinationId == destinationId 
                && accom.IsAvailable == true).ToList();
                return accommodationOptions;
                
            }
            catch(Exception ex)
            {
                Console.WriteLine("An error occured while searching for available accommodation for " +
                    $"destination with Id: {destinationId}");
                throw;
            }
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
