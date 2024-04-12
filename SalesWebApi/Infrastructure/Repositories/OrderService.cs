using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class OrderService : IOrderService
    {

        private readonly SalesDbContext _context;

        public OrderService(SalesDbContext context)
        {
            _context = context;
        }
        public void CreateOrder(int customerId, int paymentMethodId)
        {
            try
            {
                var order = new Order();
                order.customerId = customerId;
                order.PaymentMethodId = paymentMethodId;
                _context.Orders.Add(order);
                _context.SaveChanges();
                
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while creating new order. {ex.Message}");
            }
        }
    }
}
