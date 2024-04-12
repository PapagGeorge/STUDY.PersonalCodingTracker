using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        void CreateOrder(int customerId, int paymentMethodId);
    }
}
