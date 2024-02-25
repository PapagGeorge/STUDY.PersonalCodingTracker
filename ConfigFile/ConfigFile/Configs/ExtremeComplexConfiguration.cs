﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConfigFile.Configs
{
    public class ExtremeConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get => this["name"].ToString() ?? string.Empty;
            set => this["name"] = value;
        }
        [ConfigurationProperty("doom")]
        public int Doom
        {
            get => Convert.ToInt32(this["doom"].ToString());
            set => this["doom"] = value;
        }
        [ConfigurationProperty("exchange")]
        public Exchange Exchange
        {
            get => (Exchange)this["exchange"];
            set => this["exchange"] = value;
        }

        [ConfigurationProperty ("queue")]
        public Queue Queue
        {
            get => (Queue)this["queue"];
            set => this["queue"] = value;
        }


    }

    public class ExtremeConfigurationCollection : ConfigurationElementCollection
    {
        public ExtremeConfigurationElement this[string name]
        {
            get => (ExtremeConfigurationElement)BaseGet(name);

            set
            {
                if (BaseGet(name) != null)
                {
                    BaseRemove(name);
                }
                BaseAdd(value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ExtremeConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ExtremeConfigurationElement)element).Name;
        }
    }

    public class ExtremeConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("ExtremeConfigurations")]
        [ConfigurationCollection(typeof(ExtremeConfigurationCollection), AddItemName = "add")]
        public ExtremeConfigurationCollection ExtremeConfigurations
            => (ExtremeConfigurationCollection)this["ExtremeConfigurations"];
    }
}
