using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Student
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public bool IsCool {  get; set; }
    }
}
