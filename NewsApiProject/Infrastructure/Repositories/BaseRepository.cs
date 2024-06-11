using System.Configuration;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        protected SqlConnection GetSqlConnection()
        {
            var dbConfiguration = ConfigurationManager.GetSection("DatabaseConfigurationSection") as DatabaseConfiguration;
            var connection = new SqlConnection(dbConfiguration.ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
