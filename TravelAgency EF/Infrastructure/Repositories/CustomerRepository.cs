using Application.Interfaces;
using Domain.Entities;
using Domain;

namespace Infrastructure
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly TravelAgencyDbContext context;

        public CustomerRepository(TravelAgencyDbContext _context)
        {
            context = _context;
        }


        public void CreateCustomer(Customer customer)
        {
            try
            {
                context.Customers.Add(customer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured while trying to add new customer. {ex.Message}");
                throw;
            }
        }

        public Customer SearchCustomerById(long customerId)
        {
            try
            {
                var customer = context.Customers.First(cust => cust.CustomerId == customerId);
                return customer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured while searching customer by Id. {ex.Message}");
                throw;
            }
        }

        public IEnumerable<Customer> SearchCustomersByMobile(string searchText)
        {
            try
            {
                var customers = context.Customers.Where(cust => cust.MobilePhone.Contains(searchText)).ToList();

                return customers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured while searching customers by mobile phone. {ex.Message}");
                throw;
            }
        }

        public void SoftDeleteCustomer(long customerId)
        {
            try
            {
                var customer = context.Customers.First(cust => cust.CustomerId == customerId);
                if (customer != null)
                {
                    customer.IsDeleted = true;
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"No customer with Id: {customerId} exists");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured while deleting customer. {ex.Message}");
                throw;
            }
        }

        public bool CustomerExists(long customerId)
        {
            try
            {

                return context.Customers.Any(cust => cust.CustomerId == customerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured while checking if customer exists. {ex.Message}");
                throw;
            }
        }

        public void PayInvoice(long customerId, decimal paymentAmount, long invoiceId)
        {
            using(var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var invoice = context.Invoices.First(inv => inv.InvoiceId == invoiceId);

                    if (paymentAmount == invoice.TotalAmount)
                    {
                        var payment = new Payment();
                        payment.CustomerId = customerId;
                        payment.InvoiceId = invoiceId;
                        payment.Amount = paymentAmount;

                        var customer = context.Customers.First(cust => cust.CustomerId == customerId);
                        customer.Balance -= paymentAmount;


                        invoice.IsPaid = true;
                        invoice.PaymentDate = DateTime.Now;

                        context.Add(payment);
                        context.Add(invoice);
                        context.Add(payment);
                        context.Add(customer);

                        context.SaveChanges();

                        transaction.Commit();
                    }

                    if (paymentAmount > invoice.TotalAmount)
                    {
                        Console.WriteLine("The payment amount exceeds the amount required. Please try again.");
                    }

                    if (paymentAmount < invoice.TotalAmount)
                    {
                        Console.WriteLine("The payment amount is less than the amount required. Please try again.");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occured while attempting payment for invoice: {invoiceId}. {ex.Message}");
                    throw;
                }
            }
        }
    }
}
