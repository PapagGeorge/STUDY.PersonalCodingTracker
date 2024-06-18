using System.Configuration;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        protected async Task<SqlConnection> GetSqlConnectionAsync()
        {
            var dbConfiguration = ConfigurationManager.GetSection("DatabaseConfigurationSection") as DatabaseConfiguration;
            var connection = new SqlConnection(dbConfiguration.ConnectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
