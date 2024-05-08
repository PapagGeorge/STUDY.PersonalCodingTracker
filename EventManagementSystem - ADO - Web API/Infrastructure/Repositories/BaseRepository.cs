using System.Configuration;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        private readonly DatabaseConfiguration _dataBaseConfiguration;
        public BaseRepository(DatabaseConfiguration dataBaseConfiguration)
        {
            _databaseConfiguration = databaseConfiguration;
        }

        protected SqlConnection GetSqlConnection()
        {
            var connection = new SqlConnection(_dataBaseConfiguration.ConnectionString);
            connection.Open();
            return connection;
        }


    }
}
