using System.Collections;

namespace Application.Interfaces
{
    public interface ICrudService
    {
        IEnumerable GetAll<TEntity>(string tableName);
        void SoftDelete<TEntity>(string tableName, int primaryKeyValue);
        TEntity GetById<TEntity>(int id, string tableName, string columnName);
        void Insert<TEntity>(TEntity entity);
    }
}
