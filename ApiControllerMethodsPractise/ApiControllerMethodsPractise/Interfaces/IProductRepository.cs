using ApiControllerMethodsPractise.Models;

namespace ApiControllerMethodsPractise.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
    }
}
