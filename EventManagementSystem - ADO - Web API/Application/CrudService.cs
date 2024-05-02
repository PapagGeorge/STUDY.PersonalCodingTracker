﻿using Application.Interfaces;
using System.Collections;

namespace Application
{
    public class CrudService : ICrudService
    {
        private readonly IGenericRepository _genRepository;

        public CrudService(IGenericRepository genRepository)
        {
            _genRepository = genRepository;
        }

        public void SoftDelete<TEntity>(string tableName, int primaryKeyValue)
        {
            try
            {
                _genRepository.SoftDelete<TEntity>(tableName, primaryKeyValue);
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while retrieving data from the database. {ex.Message}");
            }
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
    }
}
