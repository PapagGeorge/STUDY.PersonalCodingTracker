using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Infrastructure.Constants;
using System.Data;

namespace Infrastructure.Repositories
{
    public class RegistrationRepository : BaseRepository, IRegistrationRepository
    {
        public RegistrationRepository(DatabaseConfiguration databaseConfiguration) : base(databaseConfiguration)
        {
            
        }

        public void BulkInsertRegistrations(IEnumerable<Registration> registrations)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand(StoredProcedures.BulkInsertRegistrations, connection);

                DataTable registrationsTable = new DataTable();
                registrationsTable.Columns.Add("EventId", typeof(Int32));
                registrationsTable.Columns.Add("UserId", typeof(Int32));
                registrationsTable.Columns.Add("RegistrationDateTime", typeof(DateTime));
                registrationsTable.Columns.Add("Status", typeof(string));
                registrationsTable.Columns.Add("isDeleted", typeof(bool));

                foreach (var registration in registrations)
                {
                    registrationsTable.Rows.Add(registration.EventId, registration.UserId,
                        registration.RegistrationDateTime, registration.Status, registration.isDeleted);
                }

                var parameter = command.Parameters.AddWithValue("@registrationsData", registrationsTable);
                parameter.SqlDbType = SqlDbType.Structured;
                parameter.TypeName = "dbo.udt_Registration";

                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<User> RegisteredUsersPerEvent(int eventId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Registration> RegistrationsPerUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
