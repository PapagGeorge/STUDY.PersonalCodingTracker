using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        void CreateOrder(Order order, List<Product> chosenProducts);
    }
}
