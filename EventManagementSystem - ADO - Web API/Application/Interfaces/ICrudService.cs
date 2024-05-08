using Domain.Entities;
using System.Collections;

namespace Application.Interfaces
{
    public interface ICrudService
    {
        IEnumerable GetAll<TEntity>(string tableName);
        void SoftDelete<TEntity>(string tableName, int id, string columnName);
        TEntity GetById<TEntity>(int id, string tableName, string columnName);
        void AddNewUser(User newUser);
        void BulkInsertUsers(IEnumerable<User> users);
        void BulkInsertRegistrations(IEnumerable<Registration> registrations);
    }
}
