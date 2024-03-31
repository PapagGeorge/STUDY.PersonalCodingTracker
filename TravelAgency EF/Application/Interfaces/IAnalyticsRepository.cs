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
        Destination TopDestination();
        IEnumerable<Accommodation> Top10Accommodations();
        Accommodation TopAccommodation();
        IEnumerable<Invoice> InvoicesByDateRange();
        IEnumerable<Invoice> PaidInvoices();
        IEnumerable<Invoice> UnPaidInvoices();
        IEnumerable<Payment> PaymentsByDateRange();
        IEnumerable<Payment> PaymentsByUser();


    }
}
