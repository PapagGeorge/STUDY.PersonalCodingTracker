using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public void BulkInsertProducts(List<Product> producstToAdd)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> ChooseProducts()
        {
            throw new NotImplementedException();
        }

        public bool ProductExists(int productId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> ShowAllProducts()
        {
            throw new NotImplementedException();
        }
    }
}
