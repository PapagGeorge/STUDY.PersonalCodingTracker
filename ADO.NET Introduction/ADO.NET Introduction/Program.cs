using System.Data.SqlClient;

namespace ADO.NET_Introduction
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string conString = "data source=DESKTOP; database=Northwind; integrated security =SSPI";

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand("select * from Customers", connection);
                    connection.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        Console.WriteLine(sdr["CompanyName"] + " " + sdr["City"] + " " + sdr["Phone"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong");
            }
            Console.ReadKey();
        }
    }
}
