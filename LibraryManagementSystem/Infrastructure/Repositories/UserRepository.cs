using Domain.Entities;
using Infrastructure.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;
using Infrastructure.Constants;
using System.Threading.Channels;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(DatabaseConfiguration databaseConfiguration) : base(databaseConfiguration)
        {
        }

        public bool CanUserRentMoreBooks(int userId)
        {
            bool canUserRentMoreBooks = false;
            
                using (var connection = GetSqlConnection())
                {
                    var command = new SqlCommand(StoredProcedures.CanUserRentMoreBooks, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter parameter = new SqlParameter()
                    {
                        ParameterName = "@UserId",
                        SqlDbType = SqlDbType.Int,
                        Value = userId
                    };
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            canUserRentMoreBooks = reader.GetBoolean(0);
                            return canUserRentMoreBooks;
                        }       
                    }
                }          
            
            return canUserRentMoreBooks;
        }

        public void DeleteUser(int userId)
        {
            using(var connection = GetSqlConnection())
            {
                var command = new SqlCommand(StoredProcedures.DeleteUser, connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter()
                {
                    ParameterName = "user_id",
                    SqlDbType = SqlDbType.Int,
                    Value = userId
                };
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }

        public void RegisterUser(User user)
        {
            using(var connection = GetSqlConnection())
            {
                var command = new SqlCommand(StoredProcedures.InsertUser, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = user.UserFirstName;
                command.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = user.UserLastName;
                command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = user.UserEmail;
                command.Parameters.Add("@MobilePhone", SqlDbType.VarChar, 50).Value = user.UserMobilePhone;
                command.Parameters.Add("@Address", SqlDbType.VarChar, 50).Value = user.UserAddress;

                command.ExecuteNonQuery();
            }
        }

        public User SearchUserById(string id)
        {
            using(var connection = GetSqlConnection())
            {
                var user = new User();
                var command = new SqlCommand(StoredProcedures.SearchUserById, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = id;

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    user.UserFirstName = reader["User_First_Name"].ToString() ?? string.Empty;
                    user.UserLastName = reader["User_Last_Name"].ToString() ?? string.Empty;
                    user.UserEmail = reader["User_Email"].ToString() ?? string.Empty;
                    user.UserMobilePhone = reader["User_Mobile_Phone"].ToString() ?? string.Empty;
                    user.UserAddress = reader["User_Address"].ToString() ?? string.Empty;
                    user.UserNumberOfBooksRented = Convert.ToInt32(reader["User_Number_Of_Books_Rented"]);
                    user.UserCanRentBooks = reader.GetBoolean(reader.GetOrdinal("User_Can_Rent_Books"));
                    
                }
                
                return user;

            }
        }

        public IEnumerable<User> SearchUsersByMobilePhone(string mobilePhone)
        {
            using( var connection = GetSqlConnection())
            {  
                var userList = new List<User>();
                var command = new SqlCommand(StoredProcedures.SearchUserByMobilePhone, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@MobilePhone", SqlDbType.VarChar, 50).Value = mobilePhone;

                var reader = command.ExecuteReader();

                while(reader.Read())
                {
                    var user = new User();
                    user.UserFirstName = reader["User_First_Name"].ToString() ?? string.Empty;
                    user.UserLastName = reader["User_Last_Name"].ToString() ?? string.Empty;
                    user.UserEmail = reader["User_Email"].ToString() ?? string.Empty;
                    user.UserMobilePhone = reader["User_Mobile_Phone"].ToString() ?? string.Empty;
                    user.UserAddress = reader["User_Address"].ToString() ?? string.Empty;
                    user.UserNumberOfBooksRented = Convert.ToInt32(reader["User_Number_Of_Books_Rented"]);
                    user.UserCanRentBooks = reader.GetBoolean(reader.GetOrdinal("User_Can_Rent_Books"));
                    userList.Add(user);
                }
                return userList;

                
            }
        }

        public void RemoveUserRentability(int userId)
        {
            using(var connection = GetSqlConnection())
            {
                var command = new SqlCommand(StoredProcedures.RemoveUserRentability, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                command.ExecuteNonQuery();

            }
        }

        public void RestoreUserRentability(int userId)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand(StoredProcedures.RestoreUserRentability, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                command.ExecuteNonQuery();

            }
        }
    }
}
