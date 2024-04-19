using WebApiMethodActionsStudy.Interfaces;
using WebApiMethodActionsStudy.Models;

namespace WebApiMethodActionsStudy.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> _products = new List<Product>()
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

        public void AddProduct(Product product)
        {
            try
            {
                _products.Add(product);
            }
            catch (Exception ex)
            {
                throw new Exception($"Product could not be added. {ex.Message}");
            }
            
        }

        public IEnumerable<Product> GetProducts()
        {
            try
            {
                return _products;
            }
            catch (Exception ex)
            {
                throw new Exception($"Products could not be fetched. {ex.Message}");
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                var productToUpdate = _products.FirstOrDefault(prod => prod.ProductId == product.ProductId);
                if (productToUpdate != null)
                {
                    
                    productToUpdate.ProductName = product.ProductName;
                    productToUpdate.Price = product.Price;
                    productToUpdate.Availability = product.Availability;
                    productToUpdate.IsAvailable = product.IsAvailable;
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occured while updating product. {ex.Message}");
            }
            
        }
    }
}
