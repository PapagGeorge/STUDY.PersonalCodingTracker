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
                var top10Accommodations = context.Accommodation
                .GroupJoin(
                context.Transaction,
                accom => accom.AccommodationId,
                trans => trans.AccommodationId,
                (accom, transGroup) => new { Accommodation = accom, TransactionCount = transGroup.Count() })
                .OrderByDescending(result => result.TransactionCount)
                .Select(result => result.Accommodation)
                .Take(10);

                return top10Accommodations;



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
                var top10Customers = context.Customers
                    .GroupJoin(context.Transaction,
                    cust => cust.CustomerId,
                    trans => trans.CustomerId,
                    (cust, transGroup) => new { Customer = cust, TransGroupCount = transGroup.Count() })
                    .OrderByDescending(result => result.TransGroupCount)
                    .Select(result => result.Customer)
                    .Take(10);

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
                var top10Destinations = context.Accommodation
                    .GroupJoin(
                        context.Transaction,
                        accom => accom.AccommodationId,
                        trans => trans.AccommodationId,
                        (accom, transGroup) => new
                        {
                            Accommodation = accom,
                            TransactionCount = transGroup.Count()
                        })
                    .GroupBy(
                        result => result.Accommodation.DestinationId,
                        (key, group) => new
                        {
                            DestinationId = key,
                            TotalTransactionCount = group.Sum(result => result.TransactionCount)
                        })
                    .OrderByDescending(result => result.TotalTransactionCount)
                    .Take(10)
                    .Join(
                        context.Destinations,
                        result => result.DestinationId,
                        dest => dest.DestinationId,
                        (result, dest) => dest)
                    .Distinct();

                return top10Destinations;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while searching for Top 10 Destinations. {ex.Message}");
            }
        }


        public IEnumerable<Service> Top10Services()
        {
            try
            {
                var top10Services = context.Transaction
                    .GroupBy(
                        trans => trans.ServiceId,
                        (key, group) => new
                        {
                            ServiceId = key,
                            TransactionCount = group.Count()
                        })
                    .OrderByDescending(result => result.TransactionCount)
                    .Take(10)
                    .Join(
                        context.Service,
                        result => result.ServiceId,
                        serv => serv.ServiceId,
                        (result, serv) => serv);

                return top10Services;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while searching for Top 10 Services. {ex.Message}");
            }
        }


        public Accommodation TopAccommodation()
        {
            try
            {
                var topAccommodation = context.Accommodation
                    .GroupJoin(
                    context.Transaction,
                    accom => accom.AccommodationId,
                    trans => trans.AccommodationId,
                    (accom, transGroup) => new { Accommodation = accom, TransactionCount = transGroup.Count() })
                    .OrderByDescending(result => result.TransactionCount)
                    .Select(result => result.Accommodation)
                    .FirstOrDefault();


                return topAccommodation;


            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while searching for Top Accommodation. {ex.Message}");
            }
        }

        public Customer TopCustomer()
        {
            try
            {
                var topCustomer = context.Customers
                    .GroupJoin(
                    context.Transaction,
                    cust => cust.CustomerId,
                    trans => trans.CustomerId,
                    (cust, trans) => new {Customer = cust, TransactionSum = trans.Sum(trans => trans.Amount)})
                    .OrderByDescending(result => result.TransactionSum)
                    .Select(result => result.Customer)
                    .FirstOrDefault();

                return topCustomer;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while searching for Top Accommodation. {ex.Message}");
            }

        }


        public Service TopService()
        {
            try
            {
                var topServiceInTransactions = context.Transaction
                    .GroupBy(serv => serv.ServiceId)
                    .OrderByDescending(group => group.Count())
                    .Select(g => g.Key)
                    .FirstOrDefault();

                var topService = context.Service.FirstOrDefault(serv => serv.ServiceId == topServiceInTransactions);

                return topService;

                
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occured while searching for Top Service. {ex.Message}");
            }
        }

        public IEnumerable<Transaction> TransactionsByCustomer(long customerId)
        {
            try
            {
                var transactionsByCustomer = context.Transaction
                    .Where(trans => trans.CustomerId == customerId);

                return transactionsByCustomer;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while searching for transactions by customer. {ex.Message}");
            }
        }




    }

        public IEnumerable<Invoice> UnPaidInvoices(DateTime dateRangeStart, DateTime dateRangeEnd)
        {
            throw new NotImplementedException();
        }
    }
}
