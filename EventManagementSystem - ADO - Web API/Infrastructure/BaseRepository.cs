using System.Configuration;
using System.Data.SqlClient;

namespace Infrastructure
{
    public abstract class BaseRepository
    {
        private readonly DataBaseConfiguration _dataBaseConfiguration;
        public BaseRepository(DataBaseConfiguration dataBaseConfiguration)
        {
            _dataBaseConfiguration = dataBaseConfiguration;
        }

        protected SqlConnection GetSqlConnection()
        {
            var connection = new SqlConnection(_dataBaseConfiguration.ConnectionString);
            return connection;
        }

        
    }
}
