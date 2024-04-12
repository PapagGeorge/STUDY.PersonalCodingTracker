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
        public void CreateOrder(Order order, List<Product> chosenProducts)
        {
            try
            {
               using (var transaction = _context.Database.BeginTransaction())
                {

                    
                    _context.Orders.Add(order);
                    _context.SaveChanges();


                    var orderProducts = new List<OrderProduct>();

                    foreach (var product in chosenProducts)
                    {
                        var orderProduct = new OrderProduct();
                        orderProduct.ProductId = product.ProductId;
                        orderProduct.OrderId = order.OrderId;

                        orderProducts.Add(orderProduct);
                    }

                    _context.OrderProduct.AddRange(orderProducts);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                
                
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while creating new order. {ex.Message}");
            }
        }
    }
}
