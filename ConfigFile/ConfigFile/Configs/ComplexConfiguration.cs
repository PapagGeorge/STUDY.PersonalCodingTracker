using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConfigFile.Configs
{
    public class ComplexConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get => this["name"].ToString() ?? string.Empty;
            set => this["name"] =value;
        }

        [ConfigurationProperty ("doom")]
        public int Doom
        {
            get => Convert.ToInt32(this["doom"].ToString());
            set => this["doom"] = value;

        }

    }

    public class ComplexConfigurationCollection : ConfigurationElementCollection
    {
        public new ComplexConfigurationElement this[string name]
        {
            get => (ComplexConfigurationElement) BaseGet(name);
            set
            {
                if(name != null)
                {
                    BaseRemove(name);
                }
                BaseAdd(value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ComplexConfigurationElement();
        }

        protected override object GetElementKey (ConfigurationElement element)
        {
            return ((ComplexConfigurationElement)element).Name;
        }


    }

    public class ComplexConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("ComplexConfigurations")]
        [ConfigurationCollection(typeof(ComplexConfigurationCollection), AddItemName = "add")]
        public ComplexConfigurationCollection ConfigurationCollections
            => (ComplexConfigurationCollection)this["ComplexConfigurations"];
    }
}
