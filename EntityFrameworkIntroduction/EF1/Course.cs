namespace EF1
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }

        // Navigation property representing the many-to-many relationship
        public virtual ICollection<Student> Students { get; set; }
    }
}
