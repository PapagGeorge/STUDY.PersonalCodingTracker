using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SalesDbContext _context;

        public ProductRepository(SalesDbContext context)
        {
            _context = context;
        }
        public void BulkInsertProducts(List<Product> productsToAdd)
        {
            try
            {
                _context.Products.AddRange(productsToAdd);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while inserting products. {ex.Message}");
            }
        }

        public IEnumerable<Product> ChooseProducts(List<int> productIds)
        {
            try
            {
                List<Product> chosenProducts = new List<Product>();

                foreach (int productId in productIds)
                {
                    if (ProductExists(productId))
                    {
                        chosenProducts.Add(_context.Products.FirstOrDefault(p => p.ProductId == productId));
                    }
                    else
                    {
                        throw new KeyNotFoundException($"Product with Id: {productId} was not found. Please try again.");
                    }
                }

                return chosenProducts;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while inserting products. {ex.Message}");
            }

        }

        public bool ProductExists(int productId)
        {
            try
            {
                return _context.Products.Any(p => p.ProductId == productId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while inserting products. {ex.Message}");
            }
        }

        public IEnumerable<Product> ShowAllProducts()
        {
            try
            {
                return _context.Products;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while inserting products. {ex.Message}");
            }
        }
    }
}
