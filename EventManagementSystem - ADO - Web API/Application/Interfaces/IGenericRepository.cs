namespace Application.Interfaces
{
    public interface IGenericRepository
    {
        IEnumerable<TEntity> GetAll<TEntity>(string tableName);
        TEntity GetById<TEntity> (int id, string tableName, string columnName);
        void Insert<TEntity> (TEntity entity);
        void BulkInsert<TEntity>(IEnumerable<TEntity> entities);
        void Update<TEntity> (TEntity entity);
        void SoftDelete<TEntity> (string tableName, int primaryKeyValue);
    }
}
