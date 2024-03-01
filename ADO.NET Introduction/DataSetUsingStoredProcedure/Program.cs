using System.Data;
using System.Data.SqlClient;

namespace DataSetUsingStoredProcedure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string connectionString = "data source=DESKTOP; database=EmployeeDb; integrated security=SSPI";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand()
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "SpGetEmployeesByAgeDept"
                    };

                    SqlParameter param1 = new SqlParameter()
                    {
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar,
                        ParameterName = "@Age",
                        Value = 33
                    };
                    command.Parameters.Add(param1);

                    //Another way to add the parameter for the stored procedure to the command is to use the AddWithValue() method.

                    command.Parameters.AddWithValue("Dept", "HR");

                    SqlDataAdapter dataAdapter = new SqlDataAdapter()
                    {
                        SelectCommand = command
                    };

                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);

                    if (dataSet != null)
                    {
                        foreach(DataRow row in dataSet.Tables[0].Rows)
                        {
                            Console.WriteLine(row["Id"] + "    " + row["Name"] + "    " + row["Email"] + "    " + row["Mobile"] + "    " + row["Age"] + "    " + row["Department"]);
                        }
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
