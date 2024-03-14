namespace EntityFrameworkCoreIntroduction.Models
{
    public class Manager
    {
        public int ManagerId { get; set; }
        public string MngFirstName { get; set; }
        public string MngLastName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
