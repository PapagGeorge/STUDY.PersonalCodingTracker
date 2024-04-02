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

                    var transact = new Transaction();
                    transact.CustomerId = customerId;
                    transact.AccommodationId = AccomId;
                    transact.Amount = daysOfVisit * accom.PricePerPersonPerDay;

                    var invoice = new Invoice();
                    invoice.CustomerId = customerId;
                    invoice.TotalAmount = daysOfVisit * accom.PricePerPersonPerDay;

                    var customer = new Customer();
                    customer.Balance = daysOfVisit * accom.PricePerPersonPerDay;

                    context.Add(accom);
                    context.Add(invoice);
                    context.Add(transact);
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

        public void BookService(long customerId, long serviceId)
        {
            using(var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var service = context.Service.FirstOrDefault(serv => serv.ServiceId == serviceId);
                    service.Availability -= 1;

                    if (service.Availability <= 0)
                    {
                        service.isAvailable = false;
                    }

                    var transact = new Transaction();
                    transact.CustomerId = customerId;
                    transact.ServiceId = serviceId;
                    transact.Amount = service.Price;

                    var invoice = new Invoice();
                    invoice.CustomerId = customerId;
                    invoice.TotalAmount = transact.Amount;

                    var customer = new Customer();
                    customer.Balance += invoice.TotalAmount;

                    context.Add(service);
                    context.Add(transact);
                    context.Add(invoice);
                    context.Add(customer);

                    context.SaveChanges();

                    transaction.Commit();
                }
                
                    catch (Exception ex)
                {
                    Console.WriteLine($"An error occured while booking service. {ex.Message}");
                    transaction.Rollback();
                }

                
            }
        }

        public void BookTransportation(long customerId, long transportationId)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var transportation = context.Transportation.FirstOrDefault(serv => serv.TransportationId == transportationId);
                    transportation.Availability -= 1;

                    if (transportation.Availability <= 0)
                    {
                        transportation.IsAvailable = false;
                    }

                    var transact = new Transaction();
                    transact.CustomerId = customerId;
                    transact.TransportationId = transportationId;
                    transact.Amount = transportation.Price;

                    var invoice = new Invoice();
                    invoice.CustomerId = customerId;
                    invoice.TotalAmount = transact.Amount;

                    var customer = new Customer();
                    customer.Balance += invoice.TotalAmount;

                    context.Add(transportation);
                    context.Add(transact);
                    context.Add(invoice);
                    context.Add(customer);

                    context.SaveChanges();

                    transaction.Commit();
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"An error occured while booking transportation. {ex.Message}");
                    transaction.Rollback();
                }


            }
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
