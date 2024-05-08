using Application.Interfaces;
using System.Data.SqlClient;
using Infrastructure.Constants;
using System.Data;
using System.Reflection;

namespace Infrastructure.Repositories
{
    internal class GenericRepository : BaseRepository, IGenericRepository
    {
        public GenericRepository(DatabaseConfiguration dataBaseConfiguration) : base(dataBaseConfiguration)
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

        

        public void SoftDelete<TEntity>(int id, string tableName, string columnName)
        {
            try
            {
                using var connection = GetSqlConnection();
                {
                    var command = new SqlCommand(StoredProcedures.SoftDelete, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TableName", tableName);
                    command.Parameters.AddWithValue("@PrimaryKeyValue", id);
                    command.Parameters.AddWithValue("@ColumnName", columnName);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to do a soft delete to a record. {ex.Message}");
            }
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
                throw new Exception($"An error occured while operating against the database in order to retrieve all objects of an entity. {ex.Message}");
            }
        }

        public TEntity GetById<TEntity>(int id, string tableName, string columnName)
        {
            try
            {
                var entity = Activator.CreateInstance<TEntity>();
                using var connection = GetSqlConnection();
                {
                    var command = new SqlCommand(StoredProcedures.GetById, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@TableName", tableName);
                    command.Parameters.AddWithValue("@ColumnName", columnName);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        entity = MapDataReaderToEntity<TEntity>(reader);
                    }
                }

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while operating against the database in order to find an object by id. {ex.Message}");
            }
        }

    }
}
