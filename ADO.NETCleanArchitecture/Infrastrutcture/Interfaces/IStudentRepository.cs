using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastrutcture.Interfaces
{
    public interface IStudentRepository
    {
        Student GetStudentWithProcedure(int id);
        Student GetStudentWithText(int id);
        IEnumerable<Student> GetAllStudentsWithProcedure();
        IEnumerable<Student> GetAllStudentsWithText();
        IEnumerable<Student> GetCoolStudentsWithProcedure();
        IEnumerable<Student> GetCoolStudentsWithText();
        void InsertStudentWithProcedure(Student student);
        void InsertStudentWithText(Student student);
        void UpdateStudentWithProcedure(Student student);
        void UpdateStudentWithText(Student student);
        void BulkInsertStudentsWithProcedure(IEnumerable<Student> students);
        void BulkInsertStudentsWithText(IEnumerable<Student> students);
        void HardDeleteAStudentWithProcedure(int id);
        void HardDeleteAStudentWithText(int id);
        void SoftDeleteAStudentWithProcedure(int id);
        void SoftDeleteAStudentWithText(int id);
    }
}
