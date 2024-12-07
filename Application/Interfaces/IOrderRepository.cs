using Domain;

namespace Application.Interfaces;

public interface IOrderRepository
{
    Task<Order> CreateOrderAsync(Order order);
    // Read
    Task<Order> GetOrderByIdAsync(Guid id);
    Task<IEnumerable<Order>> GetOrdersAsync(int pageNumber, int pageSize);
    Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId);
    // Update
    Task<Order> UpdateOrderAsync(Order order);
}