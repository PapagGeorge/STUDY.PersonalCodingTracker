using Application.Interfaces;
using System.Data.SqlClient;
using Infrastructure.Constants;
using System.Data;
using System.Reflection;

namespace Infrastructure.Repositories
{
    internal class GenericRepository : BaseRepository, IGenericRepository
    {
        public GenericRepository(DataBaseConfiguration dataBaseConfiguration) : base(dataBaseConfiguration)
        {
            
        }
        private TEntity MapDataReaderToEntity<TEntity>(SqlDataReader reader)
        {
            // Create an instance of TEntity
            TEntity entity = Activator.CreateInstance<TEntity>();

            // Get properties of TEntity using reflection
            PropertyInfo[] properties = typeof(TEntity).GetProperties();

            // Map data from SqlDataReader to TEntity
            foreach (PropertyInfo property in properties)
            {
                // Check if the column exists in the SqlDataReader
                int ordinal = reader.GetOrdinal(property.Name);
                if (ordinal != -1 && !reader.IsDBNull(ordinal))
                {
                    // Set the value of the property based on the column value
                    object value = reader.GetValue(ordinal);
                    property.SetValue(entity, value);
                }
            }

            return entity;
        }

        public void BulkInsert<TEntity>(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete<TEntity>(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll<TEntity>(string tableName)
        {
            try
            {
                List<TEntity> entityList = new List<TEntity>();
                using var connection = GetSqlConnection();
                {
                    var command = new SqlCommand(StoredProcedures.GetAll, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TableName", tableName);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        TEntity entity = MapDataReaderToEntity<TEntity>(reader); // Map data to TEntity object
                        entityList.Add(entity); // Add TEntity object to list
                    }
                }
                return entityList;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while operating against the database in order to retrieve all objects of an entity.");
            }
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
