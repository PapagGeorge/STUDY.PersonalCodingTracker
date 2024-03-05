using System.Configuration;

namespace Infrastrutcture
{
    public class DataBaseConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("connectionString")]
        public string ConnectionString
        {
            get=> this["connectionString"].ToString() ?? string.Empty;
            set=> this["connectionString"] = value;
        }
    }
}
