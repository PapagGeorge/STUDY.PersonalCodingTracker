using Domain.Entities;

namespace Application.Interfaces
{
    public interface IApplication
    {
       
        void CreateAnOrder(int customerId, int paymentMethodId, List<Product> productList);
        void CreateNewCustomer(Customer customer);
        IEnumerable<object> ShowCustomerOrders(int customerId);

    }
}
