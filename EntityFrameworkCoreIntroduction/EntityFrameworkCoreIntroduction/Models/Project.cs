namespace EntityFrameworkCoreIntroduction.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
