namespace EntityFrameworkIntroduction
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SchoolDbContext())
            {
                //Create
                Student Student1 = new Student { FirstName = "John", LastName = "Coltrane", Age = 38, StudentId = 1 };
                context.Students.Add(Student1);
                context.SaveChanges();

                //Read
                ListStudents(context.Students.ToList());

                //Update
                Student StudentToBeUpdated = context.Students.First();
                
                if(StudentToBeUpdated != null)
                {
                    StudentToBeUpdated.FirstName = "Mike";
                    context.SaveChanges();

                }

                //Delete
                var StudentToBeDeleted = context.Students.First();
                if(StudentToBeDeleted != null)
                {
                    context.Students.Remove(StudentToBeDeleted);
                    context.SaveChanges();
                }



                
            }

            static void ListStudents(List<Student> students)
            {
                foreach (Student student in students)
                {
                    Console.WriteLine($"ID: {student.StudentId}, Name: {student.FirstName} {student.LastName}, Age: {student.Age}");
                }
                Console.WriteLine();
            }
        }
    }
}
