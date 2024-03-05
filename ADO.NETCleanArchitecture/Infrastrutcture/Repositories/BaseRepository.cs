using Microsoft.Data.SqlClient;

namespace Infrastrutcture.Repositories
{
    public abstract class BaseRepository
    {
        private readonly DataBaseConfiguration _databaseConfiguration;
        protected BaseRepository (DataBaseConfiguration databaseConfiguration)
        {
            _databaseConfiguration = databaseConfiguration;
        }
        protected SqlConnection GetSqlConnection()
        {
            var connection = new SqlConnection(_databaseConfiguration.ConnectionString);
            connection.Open();
            return connection;
        }
    }

}
