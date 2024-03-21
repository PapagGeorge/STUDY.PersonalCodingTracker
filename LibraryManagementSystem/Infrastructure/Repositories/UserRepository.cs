using Domain.Entities;
using Infrastructure.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;
using Infrastructure.Constants;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(DatabaseConfiguration databaseConfiguration) : base(databaseConfiguration)
        {
        }

        public bool CanUserRentMoreBooks(int userId)
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

                var reader = command.ExecuteReader();

                while(reader.Read())
                {
                    int canUserRentBooks = reader.GetInt32(reader.GetOrdinal("User_Can_Rent_Books"));
                    if (canUserRentBooks >= 2)
                    {
                        return false;
                    }

                }
                return true;



                
                
            }
        }

        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public void RegisterUser(User user)
        {
            throw new NotImplementedException();
        }

        public User SearchUserById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> SearchUsersByMobilePhone(string mobilePhone)
        {
            throw new NotImplementedException();
        }
    }
}
