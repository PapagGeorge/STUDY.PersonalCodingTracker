using System.Configuration;

namespace ConfigurationFile
{
    public class Address : ConfigurationElement
    {
        [ConfigurationProperty ("StreetNumber")]
        public int StreetNumber
        {
            get => Convert.ToInt32(this["StreetNumber"].ToString());
            set => this["StreetNumber"] = value;
        }

        [ConfigurationProperty ("RoomNumber")]
        public int RoomNumber
        {
            get => Convert.ToInt32(this["RoomNumber"].ToString());
            set => this["RoomNumber"] = value;
        }

    }
}
