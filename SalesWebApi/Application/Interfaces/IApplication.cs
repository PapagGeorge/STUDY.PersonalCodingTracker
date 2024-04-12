using Domain.Entities;

namespace Application.Interfaces
{
    public interface IApplication
    {
        IEnumerable<Product> ShowProducts();
        void CreateAnOrder(int customerId, int paymentMethodId, List<Product> productList);
        void CreateNewCustomer(Customer customer);
        IEnumerable<Order> ShowCustomerOrders(int customerId);

    }
}
