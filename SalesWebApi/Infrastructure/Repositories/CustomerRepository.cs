using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SalesDbContext _context;

        public CustomerRepository(SalesDbContext context)
        {
            _context = context;
        }

        public void CreateNewCustomer(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while creating new customer. {ex.Message}");
            }
        }

        public bool CustomerExists(int customerId)
        {
            try
            {
                return _context.Customers.Any(cust => cust.CustomerId == customerId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteCustomer(int customerId)
        {
            try
            {
                if (CustomerExists(customerId))
                {
                    var customerToRemove = _context.Customers.FirstOrDefault(cust => cust.CustomerId == customerId);
                    _context.Remove(customerToRemove);
                    _context.SaveChanges();
                }
                else
                {
                    throw new KeyNotFoundException($"Customer with ID {customerId} does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while deleting customer. {ex.Message}");
            }
        }

        public IEnumerable<object> OrdersByCustomer(int customerId)
        {

            try
            {
                
                var result = from customer in _context.Customers
                             join order in _context.Orders
                             on customer.CustomerId equals order.customerId
                             select new
                             {
                                 CustomerName = customer.Name,
                                 OrderId = order.OrderId,
                                 Amount = order.Amount,
                                 Date = order.OrderDateTime
                             };
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to find the orders of customer with Id: {customerId}. {ex.Message}");
            }


        }

        public IEnumerable<Customer> ShowAllCustomers()
        {
            try
            {
                return _context.Customers;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to show all customers. {ex.Message}");
            }
        }

        public void UpdateCustomer(int customerId, Customer customer)
        {
            try
            {
                if (CustomerExists(customerId))
                {
                    var customerToUpdate = _context.Customers.FirstOrDefault(cust => cust.CustomerId == customerId);
                    customerToUpdate.Name = customer.Name;
                    customerToUpdate.Address = customer.Address;
                    customerToUpdate.MobilePhone = customer.MobilePhone;
                    customerToUpdate.Email = customer.Email;

                    _context.SaveChanges();
                }
                else
                {
                    throw new KeyNotFoundException($"Customer with ID {customerId} does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to update customer with Id: {customerId}. {ex.Message}");
            }

        }
    }
}
