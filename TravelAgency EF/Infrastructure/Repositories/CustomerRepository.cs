using Application.Interfaces;
using Domain.Entities;
using Domain;

namespace Infrastructure.Repositories
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
                var customer = context.Customers.FirstOrDefault(cust => cust.CustomerId == customerId);
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
                var customer = context.Customers.FirstOrDefault(cust => cust.CustomerId == customerId);
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
                Console.WriteLine($"An error occured while deleting customer. {ex.Message}");
                throw;
            }
        }
    }
}
