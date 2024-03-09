using System.Collections.Generic;
using TCPData;
using TCPExtensions;
using System.Linq;


namespace LinqPractise
{
    public class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine("----------Filter() Extension Method Called----------");
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





            //Console.WriteLine("----------Filter() Extension Method Called----------");

            //List <Department> departments = Data.GetDepartments();

            //var filteredDepartments = departments.Filter(dpt => dpt.ShortName == "HR");

            //foreach (var department in filteredDepartments)
            //{
            //    Console.WriteLine($"Department Id: { department.Id}");
            //    Console.WriteLine($"Departments Short Name: {department.ShortName}");
            //    Console.WriteLine($"Separtment Long Name: {department.LongName}");
            //}





            //Console.WriteLine("----------Using Where()----------");
            //List<Department> departments = Data.GetDepartments();
            //var filteredDepartments = departments.Where(dpt => dpt.ShortName == "HR" || dpt.ShortName == "TE");

            //foreach (var department in filteredDepartments)
            //{
            //    Console.WriteLine($"Department Id: {department.Id}");
            //    Console.WriteLine($"Departments Short Name: {department.ShortName}");
            //    Console.WriteLine($"Separtment Long Name: {department.LongName}");
            //    Console.WriteLine();
            //}




            //Console.WriteLine("----------Linq query syntax----------");
            //List<Employee> employees = Data.GetEmployees();
            //List<Department> departments = Data.GetDepartments();

            //var resultList = from emp in employees
            //                 join dpt in departments
            //                 on emp.DepartmentId equals dpt.Id
            //                 select new
            //                 {
            //                     FirstName = emp.FirstName,
            //                     LastName = emp.LastName,
            //                     AnnualSalary = emp.AnnualSalary,
            //                     Manager = emp.IsManager,
            //                     Department = dpt.LongName
            //                 };
            //foreach (var employee in resultList)
            //{
            //    Console.WriteLine($"First Name: {employee.FirstName}");
            //    Console.WriteLine($"Last Name: {employee.LastName}");
            //    Console.WriteLine($"Annual Salary: {employee.AnnualSalary}");
            //    Console.WriteLine($"Is Manager: {employee.Manager}");
            //    Console.WriteLine($"Department Id: {employee.Department}");
            //    Console.WriteLine();
            //}
            //Console.WriteLine();
            //var averageAnnualSalary = resultList.Average(emp=> emp.AnnualSalary);
            //Console.WriteLine($"Average Annual Salary: {averageAnnualSalary}");
            //Console.WriteLine();
            //var maxAnnualSalary = resultList.Max(emp =>  emp.AnnualSalary);
            //Console.WriteLine($"Max Annual Salary: {maxAnnualSalary}");




            Console.WriteLine("----------Linq query syntax----------");
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();

            var results = employeeList.Select(e => new
            {
                FullName = e.FirstName + " " + e.LastName,
                AnnualSalary = e.AnnualSalary
            }).Where(e => e.AnnualSalary > 40000);

            foreach (var result in results)
            {
                Console.WriteLine($"Full Name: {result.FullName}, Annual Salary: {result.AnnualSalary}");
            }
        }
    }
}
