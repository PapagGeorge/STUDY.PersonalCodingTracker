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
            var dbConfiguration = (DatabaseConfiguration)ConfigurationManager.GetSection("DatabaseConfigurationSection");
            var connection = new SqlConnection(dbConfiguration.ConnectionString);
            connection.Open();
            return connection;
        }


    }
}
