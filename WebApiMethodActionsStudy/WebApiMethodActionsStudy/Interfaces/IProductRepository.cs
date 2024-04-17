using WebApiMethodActionsStudy.Models;

namespace WebApiMethodActionsStudy.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
    }
}
