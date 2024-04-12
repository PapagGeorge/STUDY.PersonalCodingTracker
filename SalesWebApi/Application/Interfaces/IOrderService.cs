using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Order CreateOrder(int customerId, int paymentMethodId);
    }
}
