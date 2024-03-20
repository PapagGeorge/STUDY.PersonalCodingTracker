using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        private readonly DatabaseConfiguration _databaseConfiguration;
        protected BaseRepository(DatabaseConfiguration databaseConfiguration)
        {
            _databaseConfiguration = databaseConfiguration;
        }

        SqlConnection GetSqlConnection()
        {
            var connection = new SqlConnection(_databaseConfiguration.ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
