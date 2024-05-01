namespace Application.Interfaces
{
    public interface IGenericRepository
    {
        IEnumerable<TEntity> GetAll<TEntity>();
        TEntity GetById<TEntity> (int id);
        void Insert<TEntity> (TEntity entity);
        void BulkInsert<TEntity>(IEnumerable<TEntity> entities);
        void Update<TEntity> (TEntity entity);
        void Delete<TEntity> (int id);
    }
}
