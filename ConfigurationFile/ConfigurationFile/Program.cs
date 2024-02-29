using System.Configuration;
namespace ConfigurationFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FirstConfiguration firstConfiguration = ((FirstConfiguration)ConfigurationManager.GetSection("FirstConfigurationSection"));
            if (firstConfiguration != null && firstConfiguration.simpleConfigurations != null)
            {
                foreach(FirstConfigurationElement item in firstConfiguration.simpleConfigurations)
                {
                    Console.WriteLine(item.Name);
                    Console.WriteLine(item.Id);
                    Console.WriteLine(item.AddressProperty.RoomNumber);
                    Console.WriteLine(item.AddressProperty.StreetNumber);
                }
            }
            else
            {
                Console.WriteLine("Failed to retrieve custom configuration section.");
            }
        }
    }
}
