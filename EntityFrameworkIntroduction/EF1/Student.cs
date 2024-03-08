namespace EF1
{
    public class Student
    {
        public int StudentId {  get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        // Navigation property representing the many-to-many relationship
        public virtual ICollection<Course> Courses { get; set; }
    }
}
