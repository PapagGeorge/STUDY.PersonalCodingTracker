using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConfigurationFile
{
    public class FirstConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty ("id", IsRequired = true, IsKey = true)]
        public int Id
        {
            get => Convert.ToInt32(this["id"].ToString());
            set => this["id"] = value;
        }

        [ConfigurationProperty("name")]
        public string Name
        {
            get => this["name"].ToString() ?? string.Empty;
            set => this["name"] = value;
        }

        [ConfigurationProperty ("address")]
        public Address AddressProperty
        {
            get => (Address)this["address"];
            set => this["address"] = value;
        }

    }

    public class FirstConfigurationCollection : ConfigurationElementCollection
    {
        public new FirstConfigurationElement this[int Id]
        {
            get => (FirstConfigurationElement)BaseGet(Id);
            set
            {
                if(BaseGet(Id) != null)
                {
                    BaseRemove(Id);
                }
                BaseAdd(value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new FirstConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FirstConfigurationElement)element).Id;
        }
    }

    public class FirstConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("FirstConfigurations")]
        [ConfigurationCollection(typeof(FirstConfigurationCollection), AddItemName = "add")]

        public FirstConfigurationCollection simpleConfigurations =>
            ((FirstConfigurationCollection)this["FirstConfigurations"]);
    }
}
