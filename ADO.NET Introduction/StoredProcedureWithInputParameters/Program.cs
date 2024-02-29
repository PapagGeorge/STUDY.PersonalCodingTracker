using System.Data.SqlClient;
using System.Data;

namespace StoredProcedureWithInputParameters
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
                    SqlCommand sqlCommand = new SqlCommand("spGetStudentsById", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    SqlParameter param1 = new SqlParameter
                    {
                        ParameterName = "@Id",
                        SqlDbType = SqlDbType.Int,
                        Value = 100,
                        Direction = ParameterDirection.Input
                    };

                    sqlCommand.Parameters.Add(param1);
                    sqlConnection.Open();
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Console.WriteLine(dataReader["Id"] + " " + dataReader["Name"] + " " + dataReader["Email"] + " " + dataReader["Mobile"]);
                    }
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
         }
            
                    
    }
}
