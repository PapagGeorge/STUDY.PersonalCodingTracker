
using ApiControllerMethodsPractise.Interfaces;

namespace ApiControllerMethodsPractise.Models
{
    public class Product : IProductRepository
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public decimal Price { get; set; }
        public int Availability { get; set; }
        public bool isAvailable { get; set; }
        

        public IEnumerable<Product> GetProducts()
        {
            var productList = new List<Product>
            {
                new Product {ProductId = 1, ProductTitle = "Coca-Cola", Price = 1.30m, Availability = 10, isAvailable = true},
                new Product {ProductId = 2, ProductTitle = "Pepsi", Price = 1.10m, Availability = 8, isAvailable = true},
                new Product {ProductId = 3, ProductTitle = "Gum", Price = 1.50m, Availability = 0, isAvailable = false},
                new Product {ProductId = 4, ProductTitle = "Neswspaper", Price = 2m, Availability = 4, isAvailable = true},
                new Product {ProductId = 5, ProductTitle = "Chocolate", Price = 4m, Availability = 7, isAvailable = true},
                new Product {ProductId = 6, ProductTitle = "PopCorn", Price = 4m, Availability = 0, isAvailable = false},
                new Product {ProductId = 7, ProductTitle = "Magazine", Price = 7m, Availability = 5, isAvailable = true}
            };

            return productList;
        }

    }
}
