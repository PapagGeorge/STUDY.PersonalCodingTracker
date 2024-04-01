using Domain.Entities;
using Application.Interfaces;

namespace Infrastructure.Repositories
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        public IEnumerable<Customer> CustomersByRegion()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invoice> InvoicesByDateRange(DateTime dateRangeStart, DateTime dateRangeEnd)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invoice> PaidInvoices()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Payment> PaymentsByCustomer(long userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Payment> PaymentsByDateRange(DateTime dateRangeStart, DateTime dateRangeEnd)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Accommodation> Top10Accommodations()
        {
            throw new NotImplementedException();
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

        public IEnumerable<Invoice> UnPaidInvoices()
        {
            throw new NotImplementedException();
        }
    }
}
