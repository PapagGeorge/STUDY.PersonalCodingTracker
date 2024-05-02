using Application.Interfaces;
using System.Collections;

namespace Application
{
    public class CrudService : ICrudService
    {
        private readonly IGenericRepository _genRepository;

        public CrudService(IGenericRepository genRepository)
        {
            _genRepository = genRepository;
        }
        public IEnumerable GetAll<TEntity>(string tableName)
        {
            try
            {
                return _genRepository.GetAll<TEntity>(tableName);
            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred while retrieving data from the database.", ex);
            }
        }
    }
}
