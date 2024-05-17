using Application.Interfaces;
using System.Data.SqlClient;
using Domain.Entities;
using Infrastructure.Constants;
using System.Data;

namespace Infrastructure.Repositories
{
    public class EventRepository : BaseRepository, IEventRepository
    {
        public EventRepository(DatabaseConfiguration databaseConfiguration) : base(databaseConfiguration)
        {
            
        }
        public void AddNewEvent(Event newEvent)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand(StoredProcedures.InsertEvent, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Description", newEvent.Description);
                command.Parameters.AddWithValue("@StartDate", newEvent.StartDate);
                command.Parameters.AddWithValue("@EndDate", newEvent.EndDate);
                command.Parameters.AddWithValue("@Location", newEvent.Location);
                command.Parameters.AddWithValue("@OrganizerId", newEvent.OrganizerId);
                command.Parameters.AddWithValue("@Capacity", newEvent.Capacity);

                command.ExecuteNonQuery();
            }
        }
    }
}
