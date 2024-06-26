﻿using System.Configuration;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        private readonly DatabaseConfiguration _databaseConfiguration;
        public BaseRepository(DatabaseConfiguration databaseConfiguration)
        {
            _databaseConfiguration = databaseConfiguration;
        }

        protected SqlConnection GetSqlConnection()
        {
            var dbConfiguration = ConfigurationManager.GetSection("DatabaseConfigurationSection") as DatabaseConfiguration;
            var connection = new SqlConnection(dbConfiguration.ConnectionString);
            connection.Open();
            return connection;
        }


    }
}
