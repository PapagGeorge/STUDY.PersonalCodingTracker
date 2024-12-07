using Domain;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class OrderRepository : IOrderRepository
{
    private readonly OrderServiceDbContext _context;

    public OrderRepository(OrderServiceDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<Order?> GetOrderByIdAsync(Guid id)
    {
        return await _context.Orders
            .Include(o => o.ShippingAddress)
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId)
    {
        return await _context.Orders
            .Include(o => o.ShippingAddress)
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync(int pageNumber, int pageSize)
    {
        return await _context.Orders
            .Include(o => o.ShippingAddress)
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    
    public async Task<Order> UpdateOrderAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return order;
    }
}