using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAnalyticsRepository
    {
        IEnumerable<Customer> Top10Customers();
        Customer TopCustomer();
        IEnumerable<Destination> Top10Destinations();
        IEnumerable<Accommodation> Top10MostVisitedAccommodations();
        Accommodation TopAccommodation();
        IEnumerable<Service> Top10Services();
        Service TopService();
        IEnumerable<Invoice> InvoicesByDateRange(DateTime dateRangeStart, DateTime dateRangeEnd);
        IEnumerable<Invoice> PaidInvoices(DateTime dateRangeStart, DateTime dateRangeEnd);
        IEnumerable<Invoice> UnPaidInvoices(DateTime dateRangeStart, DateTime dateRangeEnd);
        IEnumerable<Payment> PaymentsByDateRange(DateTime dateRangeStart, DateTime dateRangeEnd);
        IEnumerable<Payment> PaymentsByCustomer(long userId);
        IEnumerable<Transaction> TransactionsByCustomer(long customerId);
        IEnumerable<Invoice> UnpaidInvoicesByCustomer(long customerId);
        IEnumerable<Invoice> PaidInvoicesByCustomer(long customerId, DateTime dateRangeStart, DateTime dateRangeEnd);
        IEnumerable<Invoice> UnPaidInvoicesByCustomer(long customerId, DateTime dateRangeStart, DateTime dateRangeEnd);
        bool InvoiceExists(long invoiceId);



    }
}
