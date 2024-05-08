using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Constants;
using System.Data.SqlClient;
using System.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(DatabaseConfiguration dataBaseConfiguration) : base(dataBaseConfiguration)
        {
            
        }
        public void AddUser(User newUser)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand(StoredProcedures.InsertUser, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FullName", newUser.FullName);
                command.Parameters.AddWithValue("@Email", newUser.Email);
                command.Parameters.AddWithValue("@MobilePhone", newUser.MobilePhone);

                command.ExecuteNonQuery();

            }
               
        }

        public void BulkInsertUsers(IEnumerable<User> users)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand(StoredProcedures.BulkInsertUsers, connection);
                command.CommandType = CommandType.StoredProcedure;

                DataTable userTable = new DataTable();
                userTable.Columns.Add("FullName", typeof(string));
                userTable.Columns.Add("Email", typeof(string));
                userTable.Columns.Add("MobilePhone", typeof(string));

                foreach (var user in users)
                {
                    userTable.Rows.Add(user.FullName, user.Email, user.MobilePhone);
                }

                var parameter = command.Parameters.AddWithValue("@UsersData", userTable);
                parameter.SqlDbType = SqlDbType.Structured;
                parameter.TypeName = "dbo.udt_Student";

                command.ExecuteNonQuery();


            }
        }

        public User GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
