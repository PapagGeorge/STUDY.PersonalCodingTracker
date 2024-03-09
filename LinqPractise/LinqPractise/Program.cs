using System.Collections.Generic;
using TCPData;
using TCPExtensions;


namespace LinqPractise
{
    public class Program
    {
        static void Main(string[] args)
        {
            //List<Employee> employees = Data.GetEmployees();

            //var filteredEmployees = employees.Filter(emp => emp.AnnualSalary < 40000);

            //foreach(var employee in filteredEmployees)
            //{
            //    Console.WriteLine($"First Name: {employee.FirstName}");
            //    Console.WriteLine($"Last Name: {employee.LastName}");
            //    Console.WriteLine($"Annual Salary: {employee.AnnualSalary}");
            //    Console.WriteLine($"Is Manager: {employee.IsManager}");
            //    Console.WriteLine($"Department Id: {employee.Id}");
            //    Console.WriteLine();
            //}

            List <Department> departments = Data.GetDepartments();

            var filteredDepartments = departments.Filter(dpt => dpt.ShortName == "HR");

            foreach (var department in filteredDepartments)
            {
                Console.WriteLine($"Department Id: { department.Id}");
                Console.WriteLine($"Departments Short Name: {department.ShortName}");
                Console.WriteLine($"Separtment Long Name: {department.LongName}");
            }
        }
    }
}
