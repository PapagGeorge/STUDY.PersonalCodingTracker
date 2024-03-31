using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Interfaces;

namespace Infrastructure.Repositories
{
    public class AnalyticsRepository : IAnalytics
    {
        public IEnumerable<Customer> Top10Customers()
        {

        }
        public Customer TopCustomer()
        {

        }
        public IEnumerable<Destination> Top10Destinations()
        {

        }
        public Destination TopDestination()
        {

        }
        public IEnumerable<Accommodation> Top10Accommodations()
        {

        }
        public Accommodation TopAccommodation()
        {

        }
        public IEnumerable<Invoice> InvoicesByDateRange()
        {

        }
        public IEnumerable<Invoice> PaidInvoices()
        {

        }
        public IEnumerable<Invoice> UnPaidInvoices()
        {

        }
        public IEnumerable<Payment> PaymentsByDateRange()
        {

        }
        public IEnumerable<Payment> PaymentsByUser()
        {

        }

        




    }
}
