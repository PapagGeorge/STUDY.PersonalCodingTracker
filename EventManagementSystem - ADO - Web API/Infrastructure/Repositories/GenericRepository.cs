using Application.Interfaces;

namespace Infrastructure.Repositories
{
    internal class GenericRepository : IGenericRepository
    {
        public void BulkInsert<TEntity>(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete<TEntity>(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll<TEntity>()
        {
            throw new NotImplementedException();
        }

        public TEntity GetById<TEntity>(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert<TEntity>(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update<TEntity>(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
