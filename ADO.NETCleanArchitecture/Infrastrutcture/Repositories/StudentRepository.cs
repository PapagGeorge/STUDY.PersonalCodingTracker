using Domain.Entities;
using Infrastrutcture.Constants;
using Infrastrutcture.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastrutcture.Repositories
{
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        public StudentRepository(DataBaseConfiguration _databaseConfiguration) : base(_databaseConfiguration)
        {

        }

        public void BulkInsertStudentsWithProcedure(IEnumerable<Student> students)
        {
            using (var Connection = GetSqlConnection())
            {
                SqlCommand command = new SqlCommand(StoredProcedures.BulkInsertStudents, Connection);
                command.CommandType = CommandType.StoredProcedure;

                DataTable studentTable = new DataTable();
                studentTable.Columns.Add("Name", typeof(string));
                studentTable.Columns.Add("Age", typeof(int));
                studentTable.Columns.Add("IsCool", typeof(bool));

                foreach (var student in students)
                {
                    studentTable.Rows.Add(student.Name, student.Age, student.IsCool);
                }

                var parameter = command.Parameters.AddWithValue("@StudentData", studentTable);
                parameter.SqlDbType = SqlDbType.Structured;
                parameter.TypeName = "dbo.Student";
                command.ExecuteNonQuery();

            }
        }

        public void BulkInsertStudentsWithText(IEnumerable<Student> students)
        {
            DataTable studentTable = new DataTable();
            studentTable.Columns.Add("Name", typeof(string));
            studentTable.Columns.Add("Age", typeof(int));
            studentTable.Columns.Add("IsCool", typeof(bool));

            foreach (var student in students)
            {
                studentTable.Rows.Add(student.Name, student.Age, student.IsCool);
            }

            using (var connection = GetSqlConnection())
            {
                using var bulkCopy = new SqlBulkCopy(connection);

                bulkCopy.DestinationTableName = Tables.Student;
                bulkCopy.ColumnMappings.Add("Name", "Name");
                bulkCopy.ColumnMappings.Add("Age", "Age");
                bulkCopy.ColumnMappings.Add("IsCool", "IsCool");

                bulkCopy.WriteToServer(studentTable);
            }
            Console.WriteLine("Bulk Insert Completed");
        }

        public IEnumerable<Student> GetAllStudentsWithProcedure()
        {
            List<Student> students = new List<Student>();
            using (var connection = GetSqlConnection())
            {
                SqlCommand command = new SqlCommand(StoredProcedures.GetAllStudents, connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader dataReader = command.ExecuteReader();

                Func<SqlDataReader, string, bool> getBoolean = (reader, columnName) =>
                {
                    int columnIndex = reader.GetOrdinal(columnName);

                    if (!reader.IsDBNull(columnIndex))
                    {
                        return reader.GetBoolean(columnIndex);
                    }
                    else
                    {
                        return false;
                    }
                };

                while (dataReader.Read())
                {
                    Student student = new Student()
                    {
                        Name = dataReader["Name"].ToString() ?? string.Empty,
                        Age = Convert.ToInt32(dataReader["Age"]),
                        IsCool = getBoolean(dataReader, "IsCool")
                    };
                    students.Add(student);
                }
                
                return students;
            }
        }

        public IEnumerable<Student> GetAllStudentsWithText()
        {
            List<Student> students = new List<Student>();
            using (var connection = GetSqlConnection())
            {
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Students", connection);
                SqlDataReader dataReader = command.ExecuteReader();

                Func<SqlDataReader, string, bool> getBoolean = (reader, columnName) =>
                {
                    int columnIndex = dataReader.GetOrdinal(columnName);

                    if (!reader.IsDBNull(columnIndex))
                    {
                        return reader.GetBoolean(columnIndex);
                    }
                    else { return false; }
                };
                

                while(dataReader.Read())
                {
                    Student student = new Student()
                    {
                        Name = dataReader["Name"].ToString() ?? string.Empty,
                        Age = Convert.ToInt32(dataReader["Age"]),
                        IsCool = getBoolean(dataReader, "IsCool")
                    };
                    students.Add(student);
                }
                return students;
            }
        }

        public IEnumerable<Student> GetCoolStudentsWithProcedure()
        {
            List <Student> _students = new List<Student>(); 
            using(var connection = GetSqlConnection())
            {
                SqlCommand command = new SqlCommand(StoredProcedures.GetCoolStudents, connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter("@IsCool", 1);
                parameter.SqlDbType = SqlDbType.Bit;
                command.Parameters.Add(parameter);

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Student student = new Student()
                    {
                        Name = dataReader["Name"].ToString() ?? string.Empty,
                        Age = Convert.ToInt32(dataReader["Age"]),
                        IsCool = dataReader.GetBoolean(dataReader.GetOrdinal("IsCool"))
                    };
                    _students.Add(student);
                }
                return _students;
            }
        }

        public IEnumerable<Student> GetCoolStudentsWithText()
        {
            List <Student> _students = new List <Student>();
            using (var connection = GetSqlConnection())
            {
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Students WHERE IsCool=1", connection);
                SqlDataReader dataReader = command.ExecuteReader();
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Student _student = new Student()
                    {
                        Name = dataReader["Name"].ToString() ?? string.Empty,
                        Age = Convert.ToInt32(dataReader["Age"]),
                        IsCool = dataReader.GetBoolean(dataReader.GetOrdinal("IsCool"))

                    };
                    _students.Add(_student);

                }
                return _students;
            }
        }

        public Student GetStudentWithProcedure(int id)
        {
            Student _student = new Student();
            using (var connection = GetSqlConnection())
            {
                SqlCommand command = new SqlCommand(StoredProcedures.GetStudentById, connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                command.Parameters.AddWithValue("Id", id);

                SqlDataReader dataReader = command.ExecuteReader();

                while(dataReader.Read())
                { 
                    _student.Name = dataReader["Name"].ToString() ?? string.Empty;
                    _student.Age = Convert.ToInt32(dataReader["Age"]);
                    _student.IsCool = dataReader.GetBoolean(dataReader.GetOrdinal("IsCool"));
                }
            }
            return _student;
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

