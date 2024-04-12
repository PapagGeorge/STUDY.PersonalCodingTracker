using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class OrderService : IOrderService
    {
        public Order CreateOrder(int customerId, int paymentMethodId)
        {
            throw new NotImplementedException();
        }
    }
}
