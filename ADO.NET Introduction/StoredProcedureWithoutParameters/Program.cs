using System.Data;
using System.Data.SqlClient;

namespace StoredProcedureWithoutParameters
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string connectionString = "data source=DESKTOP; database=StudentDb; integrated security=SSPI";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SpGetStudents", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    sqlConnection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Console.WriteLine(dataReader["Name"] + " " + dataReader["Mobile"]);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
