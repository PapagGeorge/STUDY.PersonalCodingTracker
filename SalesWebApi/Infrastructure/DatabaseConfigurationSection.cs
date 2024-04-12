using System.Configuration;

namespace Infrastructure
{
    public class DatabaseConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("connectionString")]
        public string ConnectionString
        {
            get => this["connectionString"].ToString() ?? string.Empty;
            set => this["connectionString"] = value;
        }
    }
}
