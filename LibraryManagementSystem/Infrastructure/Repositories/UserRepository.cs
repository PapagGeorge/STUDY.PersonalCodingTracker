using Domain.Entities;
using LibraryApplication.Interfaces;
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

        public bool UserIdExists(int userId)
        {
            try
            {
                using (var connection = GetSqlConnection())
                {
                    var command = new SqlCommand(StoredProcedures.UserIdExists, connection);
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


        public int NumberOfBooksRentedByUser(int userId)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand(StoredProcedures.NumberOfBooksRentedByUser, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        var rentedBooksNumber = reader.GetInt32(reader.GetOrdinal("User_Number_Of_Books_Rented"));
                        return rentedBooksNumber;
                    }
                    else
                    {
                        throw new Exception($"There was an error while trying to find the number of books rented " +
                            $"by user with Id: {userId}.");
                    }
                }
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
                        ParameterName = "@UserId",
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
            if (string.IsNullOrEmpty(user.UserFirstName) || string.IsNullOrEmpty(user.UserLastName) || string.IsNullOrEmpty(user.UserMobilePhone))
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

        public User SearchUserById(int id)
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
                        user.UserId = Convert.ToInt32(reader["User_Id"]);
                        user.UserFirstName = reader["User_First_Name"].ToString() ?? string.Empty;
                        user.UserLastName = reader["User_Last_Name"].ToString() ?? string.Empty;
                        user.UserEmail = reader["User_Email"].ToString() ?? string.Empty;
                        user.UserMobilePhone = reader["User_Mobile_Phone"].ToString() ?? string.Empty;
                        user.UserAddress = reader["User_Address"].ToString() ?? string.Empty;
                        user.UserNumberOfBooksRented = Convert.ToInt32(reader["User_Number_Of_Books_Rented"]);
                        user.UserCanRentBooks = reader.GetBoolean(reader.GetOrdinal("User_Can_Rent_Books"));
                        return user;


                    }
                    else
                    {
                        return null;
                    }
                    


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
                        
                        return null;
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
            catch (Exception ex)
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

        public int CountUsers()
        {
            try
            {
                using (var connection = GetSqlConnection())
                {
                    var command = new SqlCommand(StoredProcedures.CountUsers, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        return reader.GetInt32(0);

                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Failed to count users from User's table");
            }
        }

        public bool UserHasRentedBookIsbn(int userId, string isbn)
        {
            try
            {
                using (var connection = GetSqlConnection())
                {
                    var command = new SqlCommand(StoredProcedures.CheckUserHasRentedBook, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Isbn", SqlDbType.VarChar, 50).Value = isbn;
                    command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        int copiesCount = reader.GetInt32(0);

                        if (copiesCount % 2 == 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to check if user with Id: {userId} has already" +
                    $" rented book with ISBN: {isbn}. {ex.Message}");
            }
        }

        public IEnumerable<User> UserList()
        {
            try
            {
                using (var connection = GetSqlConnection())
                {
                    List<User> userList = new List<User>();
                    var command = new SqlCommand(StoredProcedures.UserList, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var user = new User();
                                user.UserId = Convert.ToInt32(reader["User_Id"]);
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
                            return null;
                        }
                        return userList;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to load user list. {ex.Message}");
            }
        }
    }
}
