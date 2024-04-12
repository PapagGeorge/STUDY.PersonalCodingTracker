using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICustomerRepository
    {
        bool CustomerExists(int customerId);
        void DeleteCustomer(int customerId);
        void CreateNewCustomer (Customer customer);
        IEnumerable<Customer> ShowAllCustomers();
        void UpdateCustomer(int customerId, Customer customer);
        IEnumerable<object> OrdersByCustomer(int customerId);
    }
}
