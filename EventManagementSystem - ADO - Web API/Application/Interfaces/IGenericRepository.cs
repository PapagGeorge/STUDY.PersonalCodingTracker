namespace Application.Interfaces
{
    public interface IGenericRepository
    {
        IEnumerable<TEntity> GetAll<TEntity>();
        TEntity GetById<TEntity> (int id);
        void Insert<TEntity> (TEntity entity);
        void Update<TEntity> (TEntity entity);
        void BulkUpdate<TEntity> (IEnumerable<TEntity> entities);
        void Delete<TEntity> (int id);
    }
}
