using WebApiActionMethodsPractice.Models;

namespace WebApiActionMethodsPractice.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
    }
}
