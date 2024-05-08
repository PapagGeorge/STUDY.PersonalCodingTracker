namespace Application.Interfaces
{
    public interface IGenericRepository
    {
        IEnumerable<TEntity> GetAll<TEntity>(string tableName);
        TEntity GetById<TEntity> (int id, string tableName, string columnName);
        void SoftDelete<TEntity> (int id, string tableName, string columnName);
    }
}
