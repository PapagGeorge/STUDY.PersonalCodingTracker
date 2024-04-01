using Domain.Entities;


namespace Application.Interfaces
{
    public interface ICustomerRepository
    {
        void CreateCustomer(Customer customer);
        void SoftDeleteCustomer(long customerId);
        IEnumerable<Customer> SearchCustomersByMobile(string searchText);
        Customer SearchCustomerById(long customerId);
        bool CustomerExists (long customerId);
    }
}
