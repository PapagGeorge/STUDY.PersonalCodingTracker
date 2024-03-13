using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCoreIntroduction.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmpFirstName { get; set; }
        public string EmpLastName { get; set; }
        public long Salary { get; set; }

        public EmployeeDetails EmployeeDetails { get; set; } //reference navigation property to dependent entity
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
