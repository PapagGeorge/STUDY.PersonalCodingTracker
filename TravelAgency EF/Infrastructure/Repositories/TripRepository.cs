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

        public void BookAccommodation(long customerId, long AccommodationId, int daysOfVisit)
        {
            using (var transaction = context.Database.BeginTransaction())
                try
                {
                    var amountList = from transact in context.Transaction
                                     join accommodation in context.Accommodation
                                     on transact.AccommodationId equals accommodation.AccommodationId
                                     select new
                                     {
                                         AccommodationId = transact.AccommodationId,
                                         Amount = accommodation.PricePerPersonPerDay
                                     };

                    var record = amountList.FirstOrDefault(amount => amount.AccommodationId == AccommodationId);

                    if (record != null)
                    {
                        var amount = record.Amount * daysOfVisit;


                        var trans = new Transaction()
                        {
                            CustomerId = customerId,
                            AccommodationId = AccommodationId,
                            Amount = amount
                        };

                        var inv = new Invoice()
                        {
                            CustomerId = customerId,
                            TotalAmount = amount
                        };
                        transaction.Commit();
                    }
                    else
                    {
                        throw new Exception("Accommodation record not found.");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"An error occured while booking accommodation. {ex.Message}");
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
