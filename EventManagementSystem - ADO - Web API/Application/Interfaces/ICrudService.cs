using System.Collections;

namespace Application.Interfaces
{
    public interface ICrudService
    {
        IEnumerable GetAll<TEntity>(string tableName);
    }
}
