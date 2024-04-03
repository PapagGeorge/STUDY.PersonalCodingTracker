using Domain.Entities;
using Application.Interfaces;

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
                throw new Exception("An error occured while searching for invoices in a specific daterange.");
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
                throw new Exception("An error occured while searching for paid invoices in a specific daterange.");
            }
        }

        public IEnumerable<Payment> PaymentsByCustomer(long userId)
        {
            try
            {
                var paymentsPerCustomer = context.Payment.Where(paym => paym.CustomerId == userId);
                return paymentsPerCustomer;
            }
            catch
            {
                throw new Exception("An error occured while searching for customer payments.");
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
                throw new Exception("An error occured while searching for payments by date range.");
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
                    .Select(g => new Accommodation
                    {

                        AccommodationId = g.Key ?? 0


                    });

                return top10Accomodations;

               
            }
            catch
            {
                throw new Exception("An error occured while searching for Top 10 Accommodation.");
            }

                
        }

        public IEnumerable<Customer> Top10Customers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Destination> Top10Destinations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Service> Top10Services()
        {
            throw new NotImplementedException();
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
