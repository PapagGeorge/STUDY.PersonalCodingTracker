using Application.Interfaces;
using System.Collections;
using Domain.Entities;

namespace Application
{
    public class CrudService : ICrudService
    {
        private readonly IGenericRepository _genRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRegistrationRepository _registrationRepository;

        public CrudService(IGenericRepository genRepository, IUserRepository userRepository,
            IRegistrationRepository registrationRepository)
        {
            _genRepository = genRepository;
            _userRepository = userRepository;
            _registrationRepository = registrationRepository;
        }
        public IEnumerable GetAll<TEntity>(string tableName)
        {
            try
            {
                return _genRepository.GetAll<TEntity>(tableName);
            }
            catch (Exception ex)
            {
                
                throw new Exception($"An error occurred while retrieving data from the database. {ex.Message}");
            }
        }

        public TEntity GetById<TEntity>(int id, string tableName, string columnName)
        {
            try
            {
                return _genRepository.GetById<TEntity>(id, tableName, columnName);
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while retrieving object from database. {ex.Message}");
            }
        }


        public void SoftDelete<TEntity>(string tableName, int id, string columnName)
        {
            try
            {
                _genRepository.SoftDelete<TEntity>(id, tableName, columnName);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while performing soft delete to an object. {ex.Message}");
            }
        }

        public void AddNewUser(User newUser)
        {
            try
            {
                _userRepository.AddUser(newUser);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void BulkInsertUsers(IEnumerable<User> users)
        {
            try
            {
                _userRepository.BulkInsertUsers(users);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void BulkInsertRegistrations(IEnumerable<Registration> registrations)
        {
            try
            {
                _registrationRepository.BulkInsertRegistrations(registrations);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
