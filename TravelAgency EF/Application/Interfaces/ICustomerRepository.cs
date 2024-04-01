using Domain.Entities;


namespace Application.Interfaces
{
    public interface ICustomerRepository
    {
        void CreateCustomer(Customer customer);
        void SoftDeleteCustomer(long customerId);
        IEnumerable<Customer> SearchCustomers(string searchText);
        Customer SearchCustomerById(long customerId);
    }
}
