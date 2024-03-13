namespace EntityFrameworkCoreIntroduction.Models
{
    public class Manager
    {
        public int ManagerId { get; set; }
        public string MngFirstName { get; set; }
        public string MngLastName { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
