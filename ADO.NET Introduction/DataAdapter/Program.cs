using System.Data.SqlClient;
using System.Data;

namespace DataAdapterUsingDataTable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string connectionString = "data source=DESKTOP; database=Northwind; integrated security = SSPI";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from customers", connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    foreach(DataRow row in  dataTable.Rows)
                    {
                        Console.WriteLine(row["CompanyName"] + " " + row["Phone"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("something went wrong");
            }
        }
    }
}
