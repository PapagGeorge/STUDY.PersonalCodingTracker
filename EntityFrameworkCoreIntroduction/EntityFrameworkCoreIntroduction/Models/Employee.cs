﻿using System;
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

        public virtual EmployeeDetails EmployeeDetails { get; set; } //reference navigation property to dependent entity
        public int ManagerId { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
