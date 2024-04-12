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

        public bool isProductAvailable(int productId)
        {
            try
            {
                return _context.Products.Any(prod => prod.ProductId == productId && prod.isAvailable == true);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while checking product availability. {ex.Message}");
            }

        }

        public IEnumerable<Product> CheckProductListAvailability(List<Product> productList)
        {
            try
            {
                List<Product> chosenProducts = new List<Product>();

                foreach (Product product in chosenProducts)
                {
                    if (isProductAvailable(product.ProductId))
                    {
                        chosenProducts.Add(_context.Products.FirstOrDefault(p => p.ProductId == product.ProductId));
                    }
                    else
                    {
                        throw new KeyNotFoundException($"Product with Id: {product.ProductId} does not exist. Please try again.");
                    }
                }

                return chosenProducts;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while inserting products. {ex.Message}");
            }
        }

        public IEnumerable<Product> ChosenProductsExist(List<Product> productList)
        {
            try
            {
                List<Product> chosenProducts = new List<Product>();

                foreach (Product product in chosenProducts)
                {
                    if (ProductExists(product.ProductId))
                    {
                        chosenProducts.Add(_context.Products.FirstOrDefault(p => p.ProductId == product.ProductId));
                    }
                    else
                    {
                        throw new KeyNotFoundException($"Product with Id: {product.ProductId} was not found. Please try again.");
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
