using WebApiMethodActionsStudy.Models;

namespace WebApiMethodActionsStudy.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        void AddProduct(Product product);
        void UpdateProduct(Product product);
    }
}
