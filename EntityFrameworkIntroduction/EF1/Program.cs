using Microsoft.EntityFrameworkCore;

namespace EF1
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SchoolDbContext())
            {
                // Ensure the database is created
                context.Database.EnsureCreated();

                // Add sample data (students and courses) for demonstration
                SeedData(context);

                // Display students and their enrolled courses
                Console.WriteLine("All Students:");
                ListStudents(context.Students.Include(s => s.Courses).ToList());
            }
        }

        static void SeedData(SchoolDbContext context)
        {
            // Add sample students
            var student1 = new Student { FirstName = "John", LastName = "Doe", Age = 20 };
            var student2 = new Student { FirstName = "Jane", LastName = "Smith", Age = 22 };
            context.Students.AddRange(student1, student2);

            // Add sample courses
            var course1 = new Course { Title = "Mathematics" };
            var course2 = new Course { Title = "History" };
            context.Courses.AddRange(course1, course2);

            // Enroll students in courses
            student1.Courses.Add(course1);
            student1.Courses.Add(course2);
            student2.Courses.Add(course1);

            // Save changes to the database
            context.SaveChanges();
        }

        static void ListStudents(List<Student> students)
        {
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.StudentId}, Name: {student.FirstName} {student.LastName}, Age: {student.Age}");

                Console.WriteLine("Enrolled Courses:");
                foreach (var course in student.Courses)
                {
                    Console.WriteLine($"  - {course.Title}");
                }

                Console.WriteLine();
            }
        }
    }
}