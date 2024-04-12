using Domain.Entities;

namespace Application.Interfaces
{
    public interface IApplication
    {
        IEnumerable<Product> ShowProducts();
        void CreateAnOrder(Order order);
        void CreateNewCustomer(Customer customer);
        IEnumerable<Order> ShowCustomerOrders(int customerId);

    }
}
