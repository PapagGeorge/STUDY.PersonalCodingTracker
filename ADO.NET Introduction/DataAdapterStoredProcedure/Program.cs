using System.Data.SqlClient;
using System.Data;

namespace DataAdapterStoredProcedure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "data source=DESKTOP; database=Northwind; integrated security=SSPI";
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("spGetStudents", connection);
                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    foreach(DataRow row in dataTable.Rows)
                    {
                        Console.WriteLine(row["CompanyName"] + " " + row["Phone"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong" + ex.Message);
            }
        }
    }
}
