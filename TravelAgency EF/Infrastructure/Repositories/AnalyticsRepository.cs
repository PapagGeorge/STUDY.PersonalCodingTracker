using Domain.Entities;
using Application.Interfaces;
using Infrastructure.Migrations;

namespace Infrastructure.Repositories
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly TravelAgencyDbContext context;

        public AnalyticsRepository(TravelAgencyDbContext _context)
        {
            context = _context;
        }

        public IEnumerable<Invoice> InvoicesByDateRange(DateTime dateRangeStart, DateTime dateRangeEnd)
        {
            try
            {
                var dateRangeInvoices = context.Invoices.Where(inv => inv.IssuedDate >= dateRangeStart && inv.IssuedDate <= dateRangeEnd);
                return dateRangeInvoices;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while searching for invoices in a specific daterange. {ex.Message}");
            }
            
        }

        public IEnumerable<Invoice> PaidInvoices(DateTime dateRangeStart, DateTime dateRangeEnd)
        {
            try
            {
                var paidInvoices = context.Invoices.Where(inv => inv.IsPaid == true && inv.IssuedDate >= dateRangeStart
                && inv.IssuedDate <= dateRangeEnd);
                return paidInvoices;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while searching for paid invoices in a specific daterange. {ex.Message}");
            }
        }

        public IEnumerable<Payment> PaymentsByCustomer(long userId)
        {
            try
            {
                var paymentsPerCustomer = context.Payment.Where(paym => paym.CustomerId == userId);
                return paymentsPerCustomer;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while searching for customer payments. {ex.Message}");
            }
        }

        public IEnumerable<Payment> PaymentsByDateRange(DateTime dateRangeStart, DateTime dateRangeEnd)
        {
            try
            {
                var paymentsInDateRange = context.Payment.Where(paym => paym.PaymentDate >= dateRangeStart
                && paym.PaymentDate <= dateRangeEnd);
                return paymentsInDateRange;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while searching for payments by date range. {ex.Message}");
            }
        }

        public IEnumerable<Accommodation> Top10MostVisitedAccommodations()
        {
            try
            {
                var top10Accomodations = context.Transaction
                    .GroupBy(trans => trans.AccommodationId)
                    .OrderByDescending(g => g.Count())
                    .Take(10)
                    .Join(
                    context.Accommodation,
                    transGroup => transGroup.Key,
                    accom => accom.AccommodationId,
                    (transGroup, accom) => new Accommodation
                    {
                        AccommodationId = (long)transGroup.Key,
                        HotelName = accom.HotelName,
                        StarRating = accom.StarRating

                    });

                return top10Accomodations;

               
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while searching for Top 10 Accommodation. {ex.Message}");
            }

                
        }

        public IEnumerable<Customer> Top10Customers()
        {
            try
            {
                var top10Customers = context.Transaction
                    .GroupBy(trans => trans.CustomerId)
                    .OrderByDescending(g => g.Count())
                    .Take(10)
                    .Join(
                    context.Customers,
                    transGroup => transGroup.Key,
                    cust => cust.CustomerId,
                    (transGroup, cust) => new Customer
                    {
                        CustomerId = transGroup.Key,
                        FirstName = cust.FirstName,
                        LastName = cust.LastName,
                        Address = cust.Address,
                        City = cust.City,
                        MobilePhone = cust.MobilePhone
                    });

                return top10Customers;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while searching for Top 10 Customers. {ex.Message}");
            }
        }

        public IEnumerable<Destination> Top10Destinations()
        {
            try
            {
                var top10Destinations = context.Transaction
                    .GroupBy(trans => trans.AccommodationId)
                    .OrderByDescending(g => g.Count())
                    .Take(10)
                    .Join(
                    context.Accommodation,
                    transGroup => transGroup.Key,
                    accom => accom.AccommodationId,
                    (transGroup, accom) => new Accommodation
                    {
                        AccommodationId = transGroup.Key,
                        DestinationId = accom.DestinationId

                    }).Join(context.Destinations,
                    accommod => accommod.DestinationId,
                    dest => dest.DestinationId,
                    (accommod, dest) => new Destination
                    {
                        DestinationId = dest.DestinationId,
                        Country = dest.Country,
                        City = dest.City,
                        PostalCode = dest.PostalCode
                    });

                return top10Destinations;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while searching for Top 10 Customers. {ex.Message}");
            }
        }

        public IEnumerable<Service> Top10Services()
        {
            try
            {
                var Top10Services = context.Transaction
                    .GroupBy(trans => trans.ServiceId)
                    .OrderByDescending(group => group.Count())
                    .Take(10)
                    .Join(context.Service,
                    groupServ => groupServ.Key,
                    serv => serv.ServiceId,
                    (groupServ, serv) => new Service
                    {
                        ServiceId = groupServ.Key,
                        ServiceName = serv.ServiceName,
                        Price = serv.Price
                    });
                return Top10Services;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while searching for Top 10 Customers. {ex.Message}");
            }
        }

        public Accommodation TopAccommodation()
        {
            throw new NotImplementedException();
        }

        public Customer TopCustomer()
        {
            throw new NotImplementedException();
        }

        public Destination TopDestination()
        {
            throw new NotImplementedException();
        }

        public Accommodation TopService()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> TransactionsByCustomer(long customerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invoice> UnPaidInvoices(DateTime dateRangeStart, DateTime dateRangeEnd)
        {
            throw new NotImplementedException();
        }
    }
}
