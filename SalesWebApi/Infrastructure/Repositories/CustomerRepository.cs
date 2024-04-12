using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public void CreateNewCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public bool CustomerExists(int customerId)
        {
            throw new NotImplementedException();
        }

        public void DeleteCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> ShowAllCustomers()
        {
            throw new NotImplementedException();
        }

        public void UpdateCustomer(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
