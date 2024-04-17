using WebApiActionMethodsPractice.Interfaces;
using WebApiActionMethodsPractice.Models;

namespace WebApiActionMethodsPractice.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public IEnumerable<Product> GetAllProducts()
        {
            var productList = new List<Product>
            {
                new Product { ProductId = 1, ProductTitle = "CocaCola", Price = 1.30m, Availability = 5, IsAvailable = true },
                new Product { ProductId = 2, ProductTitle = "PepsiCola", Price = 1.10m, Availability = 10, IsAvailable = true },
                new Product { ProductId = 3, ProductTitle = "NewsPaper", Price = 3m, Availability = 3, IsAvailable = true },
                new Product { ProductId = 4, ProductTitle = "Chocolate", Price = 5m, Availability = 5, IsAvailable = true },
                new Product { ProductId = 5, ProductTitle = "PopCorn", Price = 6m, Availability = 0, IsAvailable = false },
                new Product { ProductId = 6, ProductTitle = "Cigars", Price = 9m, Availability = 0, IsAvailable = false },
                new Product { ProductId = 7, ProductTitle = "Gums", Price = 2m, Availability = 2, IsAvailable = true },
                new Product { ProductId = 8, ProductTitle = "Whiskey", Price = 15m, Availability = 2, IsAvailable = true }
            };
            return productList;
        }
    }
}
