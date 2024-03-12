using System.Diagnostics.CodeAnalysis;
using TCPData;

namespace TCPExtensions
{
    public class EmployeeComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee? x, Employee? y)
        {
            if (x.FirstName == y.FirstName && x.LastName == y.LastName && x.Id == y.Id)
            {
                return true;
            }
            return false;
  

        }

        public int GetHashCode([DisallowNull] Employee obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
