using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConfigFile.Configs
{
    public class Exchange : ConfigurationElement
    {
        [ConfigurationProperty("id")]
        public int Id
        {
            get => Convert.ToInt32(this["id"]);
            set => this["id"] = value;
        }

        [ConfigurationProperty ("room")]
        public int Room
        {
            get => Convert.ToInt32(this["room"]);
            set => this["room"] = value;
        }
    }
}
