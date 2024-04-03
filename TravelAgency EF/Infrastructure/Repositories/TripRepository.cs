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

        public bool AccommodationExists(long accommodationId)
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
                    var accom = context.Accommodation.First(accom => accom.AccommodationId == AccomId);
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
                    var service = context.Service.First(serv => serv.ServiceId == serviceId);
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
                    var transportation = context.Transportation.First(serv => serv.TransportationId == transportationId);
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

        public bool IsAccommodationAvailable(long accommodationId)
        {
            try
            {
                var accommodation = context.Accommodation.First(accom => accom.AccommodationId == accommodationId);

                return accommodation.IsAvailable;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while checking accommodation availability. {ex.Message}");
            }

        }

        public bool IsServiceAvailable(long serviceId)
        {
            try
            {

                var service = context.Service.First(serv => serv.ServiceId == serviceId);

                return service.isAvailable;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while checking service availability. {ex.Message}");
            }

        }

        public bool IsTransportationAvailable(long transportationId)
        {
            try
            {

                var transportation = context.Transportation.First(trans => trans.TransportationId == transportationId);

                return transportation.IsAvailable;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while checking transportation availability. {ex.Message}");
            }
        }

        public bool ServiceExists(long serviceId)
        {
            try
            {

                return context.Service.Count(serv => serv.ServiceId == serviceId) == 1;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while checking if service exists. {ex.Message}");
            }
        }

        public IEnumerable<Service> ServicesByDestination(long destinationId)
        {
            try
            {
                var servicesByDestination = context.Service.Where(serv => serv.DestinationId == destinationId 
                && serv.isAvailable);
                return servicesByDestination;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while checking services by destination. {ex.Message}");
            }
        }

        public IEnumerable<Destination> ShowAllDestinations()
        {
            try
            {
                var destinations = from dest in context.Destinations
                                   select dest;

                return destinations;
                
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while fetching destination list. {ex.Message}");
            }
        }

        public IEnumerable<Transportation> TransporationByDestination(long destinationId)
        {
            try
            {
                var transportationsByDestination = context.Transportation.Where(trans => trans.DestinationId == destinationId
            && trans.IsAvailable == true);
                return transportationsByDestination;

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while checking transportion by destination. {ex.Message}");
            }
            
        }

        public bool TransportationExists(long transportationId)
        {
            try
            {
                return context.Transportation.Any(trans => trans.TransportationId == transportationId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while checking if transportation exists. {ex.Message}");
            }
        }
    }
}
