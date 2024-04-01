using Application.Interfaces;
using Domain.Entities;
using Domain;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {




        public void CreateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Customer SearchCustomerById(long customerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> SearchCustomers(string searchText)
        {
            throw new NotImplementedException();
        }

        public void SoftDeleteCustomer(long customerId)
        {
            throw new NotImplementedException();
        }
    }
}
