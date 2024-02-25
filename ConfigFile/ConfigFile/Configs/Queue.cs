using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConfigFile.Configs
{
    public class Queue : ConfigurationElement
    {
        [ConfigurationProperty("pass")]
        public string Pass
        {
            get => this["pass"].ToString() ?? string.Empty;
            set => this["pass"] = value;
        }

        [ConfigurationProperty ("fire")]
        public string Fire
        {
            get => this["fire"].ToString() ?? string.Empty;
            set => this["fire"] = value;
        }


    }
}
