using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> ShowAllProducts();
        void BulkInsertProducts(List<Product> producstToAdd);
        bool ProductExists(int productId);
        IEnumerable<Product> ChosenProductsExist(List <Product> productList);
        IEnumerable<Product> CheckProductListAvailability(List<Product> productList);
        bool isProductAvailable(int productId);

    }
}
