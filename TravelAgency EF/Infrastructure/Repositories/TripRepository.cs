using Domain.Entities;
using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;



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

        public bool accommodationExists(long accommodationId)
        {
            try
            {
                return context.Accommodation.Count(acom => acom.AccommodationId == accommodationId) == 1;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to validate the accommodation. {ex.Message}");
            }

        }

        public void BookAccommodation(long customerId, long AccomId, int daysOfVisit)
        {
            using(var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var accom = context.Accommodation.FirstOrDefault(accom => accom.AccommodationId == AccomId);
                    accom.Availability -= 1;

                    if (accom.Availability <= 0)
                    {
                        accom.IsAvailable = false;
                    }

                    var invoice = new Invoice();
                    invoice.CustomerId = customerId;
                    invoice.TotalAmount = daysOfVisit * accom.PricePerPersonPerDay;

                    var trans = new Transaction();
                    trans.CustomerId = customerId;
                    trans.AccommodationId = AccomId;
                    trans.Amount = daysOfVisit * accom.PricePerPersonPerDay;

                    var customer = new Customer();
                    customer.Balance = daysOfVisit * accom.PricePerPersonPerDay;


                    context.Add(invoice);
                    context.Add(trans);
                    context.Add(customer);
                    context.SaveChanges();

                    transaction.Commit();   
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occured while booking an accomodation. {ex.Message}");
                    transaction.Rollback();
                }
                
            }

        }

        public void BookService(long customerId, long ServiceId)
        {
            throw new NotImplementedException();
        }

        public void BookTransportation(long customerId, long transportationId)
        {
            throw new NotImplementedException();
        }

        public bool isAccommodationAvailable(long accommodationId)
        {
            throw new NotImplementedException();
        }

        public bool isServiceAvailable(long serviceId)
        {
            throw new NotImplementedException();
        }

        public bool isTransportationAvailable(long TransportationId)
        {
            throw new NotImplementedException();
        }

        public bool serviceExists(long serviceId)
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

        public bool transportationExists(long transportationId)
        {
            throw new NotImplementedException();
        }
    }
}
