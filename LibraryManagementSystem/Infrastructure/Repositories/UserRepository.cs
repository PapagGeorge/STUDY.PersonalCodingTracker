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

        public bool UserIdExists(string userId)
        {
            try
            {
                using(var connection = GetSqlConnection())
                {
                    var command = new SqlCommand(StoredProcedures.SearchById, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured. {ex.Message}");
            }
        }
        public bool CanUserRentMoreBooks(int userId)
        {
            
            try
            {
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
                        if (reader.HasRows)
                        {
                            reader.Read();
                            return reader.GetBoolean(0);
                            
                        }
                        else
                        {
                            throw new Exception($"No user was found with Id: {userId}");
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured: {ex.Message}");   
            }
            
            
        }

        public void DeleteUser(int userId)
        {
            try
            {
                using (var connection = GetSqlConnection())
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
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occured while trying to delete user with id: {userId}. {ex.Message}");
            }
        }

        public void RegisterUser(User user)
        {
            if(string.IsNullOrEmpty(user.UserFirstName) || string.IsNullOrEmpty(user.UserLastName) || string.IsNullOrEmpty(user.UserMobilePhone))
            {
                Console.WriteLine("One or more required fields are missing");
                return;
            }
            try
            {
                using (var connection = GetSqlConnection())
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
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured while trying to insert new user. {ex.Message}");
            }
        }

        public User SearchUserById(string id)
        {
            try
            {
                using (var connection = GetSqlConnection())
                {
                    var user = new User();
                    var command = new SqlCommand(StoredProcedures.SearchUserById, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserId", SqlDbType.Int).Value = id;

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        user.UserFirstName = reader["User_First_Name"].ToString() ?? string.Empty;
                        user.UserLastName = reader["User_Last_Name"].ToString() ?? string.Empty;
                        user.UserEmail = reader["User_Email"].ToString() ?? string.Empty;
                        user.UserMobilePhone = reader["User_Mobile_Phone"].ToString() ?? string.Empty;
                        user.UserAddress = reader["User_Address"].ToString() ?? string.Empty;
                        user.UserNumberOfBooksRented = Convert.ToInt32(reader["User_Number_Of_Books_Rented"]);
                        user.UserCanRentBooks = reader.GetBoolean(reader.GetOrdinal("User_Can_Rent_Books"));
                        

                    }
                    else
                    {
                        throw new Exception($"No user was found with Id: {id}");
                    }
                    return user;
                    
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to search for user with Id: {id}. {ex.Message}");
                
            }
        }

        public IEnumerable<User> SearchUsersByMobilePhone(string mobilePhone)
        {
            try
            {
                using (var connection = GetSqlConnection())
                {
                    var userList = new List<User>();
                    var command = new SqlCommand(StoredProcedures.SearchUserByMobilePhone, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@MobilePhone", SqlDbType.VarChar, 50).Value = mobilePhone;

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
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

                    }
                    else
                    {
                        throw new Exception($"No user was found with Mobile Phone: {mobilePhone}");
                    }
                    return userList;

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured. {ex.Message}");
            }
        }

        public void RemoveUserRentability(int userId)
        {
            try
            {
                using (var connection = GetSqlConnection())
                {
                    var command = new SqlCommand(StoredProcedures.RemoveUserRentability, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    command.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occured. {ex.Message}");
            }
        }

        public void RestoreUserRentability(int userId)
        {
            try
            {
                using (var connection = GetSqlConnection())
                {
                    var command = new SqlCommand(StoredProcedures.RestoreUserRentability, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured. {ex.Message}");
            }
        }
    }
}
