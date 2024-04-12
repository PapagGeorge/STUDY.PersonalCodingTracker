using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> ShowAllProducts();
        void BulkInsertProducts(List<Product> producstToAdd);
        bool ProductExists(int productId);
        IEnumerable<Product> ChooseProducts(List <int> productIds);

    }
}
