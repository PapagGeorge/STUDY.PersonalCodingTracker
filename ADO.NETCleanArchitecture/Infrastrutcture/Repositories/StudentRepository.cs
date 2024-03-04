using Domain.Entities;
using Infrastrutcture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastrutcture.Repositories
{
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        public StudentRepository (DatabaseConfiguration _databaseConfiguration) : base (_databaseConfiguration)
        {

        }
        
        public void BulkInsertStudentsWithProcedure(IEnumerable<Student> students)
        {
            throw new NotImplementedException();
        }

        public void BulkInsertStudentsWithText(IEnumerable<Student> students)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetAllStudentsWithProcedure()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetAllStudentsWithText()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetCoolStudentsWithProcedure()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetCoolStudentsWithText()
        {
            throw new NotImplementedException();
        }

        public Student GetStudentWithProcedure(int id)
        {
            throw new NotImplementedException();
        }

        public Student GetStudentWithText(int id)
        {
            throw new NotImplementedException();
        }

        public void HardDeleteAStudentWithProcedure(int id)
        {
            throw new NotImplementedException();
        }

        public void HardDeleteAStudentWithText(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertStudentWithProcedure(Student student)
        {
            throw new NotImplementedException();
        }

        public void InsertStudentWithText(Student student)
        {
            throw new NotImplementedException();
        }

        public void SoftDeleteAStudentWithProcedure(int id)
        {
            throw new NotImplementedException();
        }

        public void SoftDeleteAStudentWithText(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateStudentWithProcedure(Student student)
        {
            throw new NotImplementedException();
        }

        public void UpdateStudentWithText(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
