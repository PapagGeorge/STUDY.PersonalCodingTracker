using Application.Interfaces;
using Domain.Entities;

namespace Application
{
    public class Application : IApplication
    {
        public void CreateAnOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void CreateNewCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> ShowCustomerOrders(int customerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> ShowProducts()
        {
            throw new NotImplementedException();
        }
    }
}
