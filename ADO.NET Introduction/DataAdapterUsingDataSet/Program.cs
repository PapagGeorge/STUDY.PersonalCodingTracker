using System.Data.SqlClient;
using System.Data;

namespace DataAdapterUsingDataSet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "data source=DESKTOP; database=Northwind; integrated security=SSPI";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from Customers", connection);

                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, "Customers");

                    foreach (DataRow row in dataSet.Tables["Customers"].Rows)
                    {
                        Console.WriteLine(row["CompanyName"] + " " + row["Phone"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong");
            }
            
        }
    }
}
