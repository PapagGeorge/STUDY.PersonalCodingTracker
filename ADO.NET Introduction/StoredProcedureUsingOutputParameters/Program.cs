using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace StoredProcedureUsingOutputParameters
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string connectionString = "data source=DESKTOP; database=StudentDb; integrated security=SSPI";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand()
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "SpCreateNewStudent"
                    };
                    SqlParameter param1 = new SqlParameter()
                    {
                        ParameterName = "@Name",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = "Elvin",
                        Direction = ParameterDirection.Input
                    };

                    command.Parameters.Add(param1);
                    command.Parameters.AddWithValue("Email", "jones@mail.com");
                    command.Parameters.AddWithValue("mobile", "6987846159");

                    SqlParameter outParam = new SqlParameter()
                    {
                        ParameterName = "@Id",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outParam);
                    connection.Open();
                    command.ExecuteNonQuery();

                    Console.WriteLine($"New Student Added Successfully with Id: {outParam.Value.ToString()}");



                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
