using System.Data.SqlClient;
using System.Configuration;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        private readonly DatabaseConfiguration _databaseConfiguration;

        protected BaseRepository(DatabaseConfiguration databaseConfiguration)
        {
            _databaseConfiguration = databaseConfiguration;
        }

        protected SqlConnection GetSqlConnection()
        {
            var dbConfiguration = ConfigurationManager.GetSection("DatabaseConfiguration") as DatabaseConfiguration;
            var connection = new SqlConnection(dbConfiguration.ConnectionString);
            connection.Open();
            return connection;
            
        }
    }
}
