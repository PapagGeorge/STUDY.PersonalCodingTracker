using WebApiMethodActionsStudy.Interfaces;
using WebApiMethodActionsStudy.Models;

namespace WebApiMethodActionsStudy.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public IEnumerable<Product> GetProducts()
        {
            List<Product> products = new List<Product>()
            {
                new Product { ProductId = 1, ProductName = "Coca-Cola", Price = 1.30m, Availability = 5, IsAvailable = true },
                new Product { ProductId = 2, ProductName = "Pepsi-Cola", Price = 1.10m, Availability = 5, IsAvailable = true },
                new Product { ProductId = 3, ProductName = "Cocaine", Price = 15m, Availability = 5, IsAvailable = true },
                new Product { ProductId = 4, ProductName = "Chocolate", Price = 3m, Availability = 5, IsAvailable = true },
                new Product { ProductId = 5, ProductName = "Cigars", Price = 8m, Availability = 5, IsAvailable = true },
                new Product { ProductId = 6, ProductName = "Newspaper", Price = 4m, Availability = 5, IsAvailable = true },
                new Product { ProductId = 7, ProductName = "Candies", Price = 3m, Availability = 5, IsAvailable = true },
                new Product { ProductId = 8, ProductName = "Ice Cream", Price = 8m, Availability = 5, IsAvailable = true },
                new Product { ProductId = 9, ProductName = "Bisquits", Price = 6m, Availability = 5, IsAvailable = true }
            };
            return products;
        }
    }
}
