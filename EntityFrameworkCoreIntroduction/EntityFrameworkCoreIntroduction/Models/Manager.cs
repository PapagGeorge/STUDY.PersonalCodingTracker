using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCoreIntroduction.Models
{
    public class Manager
    {
        public int ManagerId { get; set; }
        public string MngFirstName { get; set; }
        public string MngLastName { get; set; }
    }
}
