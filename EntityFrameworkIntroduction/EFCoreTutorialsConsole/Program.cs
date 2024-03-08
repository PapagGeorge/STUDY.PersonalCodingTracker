namespace EFCoreTutorialsConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SchoolDbContext())
            {
                context.Database.EnsureCreated();

                var grd1 = new Grade() { GradeName = "First Grade" };
                var std1 = new Student() { FirstName = "Yash", LastName = "Malhotra", Grade = grd1 };

                context.Students.Add(std1);
                context.SaveChanges();

                foreach (var student in context.Students)
                {
                    Console.WriteLine($"First Name: {student.FirstName}, Last Name: {student.LastName}");
                }
            }
        }
    }
}
