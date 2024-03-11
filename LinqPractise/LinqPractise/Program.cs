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




            //Console.WriteLine("----------Select and Where Operators - Method Syntax----------");
            //List<Employee> employeeList = Data.GetEmployees();
            //List<Department> departmentList = Data.GetDepartments();

            //var results = employeeList.Select(e => new
            //{
            //    FullName = e.FirstName + " " + e.LastName,
            //    AnnualSalary = e.AnnualSalary
            //}).Where(e => e.AnnualSalary > 40000);

            //foreach (var result in results)
            //{
            //    Console.WriteLine($"Full Name: {result.FullName}, Annual Salary: {result.AnnualSalary}");
            //}




            //    Console.WriteLine("----------Using Query Syntax----------");
            //    List<Employee> employeeList = Data.GetEmployees();
            //    List<Department> departmentList = Data.GetDepartments();
            //    var result = from emp in employeeList
            //                 where emp.AnnualSalary > 40000
            //                 select new
            //                 {
            //                     FullName = emp.FirstName + " " + emp.LastName,
            //                     AnnualSalary = emp.AnnualSalary

            //                 };
            //    foreach (var res in result)
            //    {
            //        Console.WriteLine($"Full Name: {res.FullName}, Annual Salary: {res.AnnualSalary}");
            //    }
            //}




            //Console.WriteLine("----------Using the Join Operator----------");
            //List<Employee> employeeList = Data.GetEmployees();
            //List<Department> departmentList = Data.GetDepartments();

            //var results = departmentList.Join(employeeList, department => department.Id, employee => employee.DepartmentId,
            //    (department, employee) => new
            //    {
            //        FullName = employee.FirstName + " " + employee.LastName,
            //        AnnualSalary = employee.AnnualSalary,
            //        DepartmentName = department.LongName

            //    });
            //foreach (var result in results)
            //{
            //    Console.WriteLine($"Full Name: {result.FullName}, Annual Salary: {result.AnnualSalary}, Department Name: {result.DepartmentName}");
            //}




            //    Console.WriteLine("----------Using query syntax for for Join operation----------");
            //    List<Employee> employeeList = Data.GetEmployees();
            //    List<Department> departmentList = Data.GetDepartments();

            //var results = from dept in departmentList
            //              join emp in employeeList
            //              on dept.Id equals emp.DepartmentId
            //              select new
            //              {
            //                  FullName = emp.FirstName + " " + emp.LastName,
            //                  AnnualSalary = emp.AnnualSalary,
            //                  DepartmentName = dept.LongName
            //              };

            //    foreach (var result in results)
            //    {
            //        Console.WriteLine($"Full Name: {result.FullName}, Annual Salary: {result.AnnualSalary}, Department Name: {result.DepartmentName}");
            //    }
            //}




            //Console.WriteLine("----------GroupJoin using Method Syntax----------");
            //List<Employee> employeeList = Data.GetEmployees();
            //List<Department> departmentList = Data.GetDepartments();

            //var results = departmentList.GroupJoin(employeeList,
            //    department => department.Id, employee => employee.DepartmentId,
            //    (department, employeesGroup) => new
            //    {
            //        Employees = employeesGroup,
            //        DepartmentName = department.LongName
            //    });

            //foreach (var result in results)
            //{
            //    Console.WriteLine($"Department Name: {result.DepartmentName}");
            //    foreach(var item in result.Employees)
            //    {
            //        Console.WriteLine($"First Name: {item.FirstName}, Last Name: {item.LastName}");
            //    }
            //}




            //Console.WriteLine("----------GroupJoin using Query Syntax----------");
            //List<Employee> employeeList = Data.GetEmployees();
            //List<Department> departmentList = Data.GetDepartments();

            //var results = from dept in departmentList
            //              join emp in employeeList
            //              on dept.Id equals emp.DepartmentId
            //              into employeeGroup
            //              select new
            //              {
            //                  Employees = employeeGroup,
            //                  Department = dept.LongName
            //              };

            //foreach (var result in results)
            //{
            //    Console.WriteLine($"Department Name: {result.Department}");
            //    foreach (var item in result.Employees)
            //    {
            //        Console.WriteLine($"First Name: {item.FirstName}, Last Name: {item.LastName}");
            //    }
            //}




            //Console.WriteLine("----------OrderBy using Method Syntax----------");
            //List<Employee> employeeList = Data.GetEmployees();
            //List<Department> departmentList = Data.GetDepartments();

            //var results = departmentList.Join(employeeList, department => department.Id, employee => employee.DepartmentId,
            //    (department, employee) => new
            //    {
            //        DepartmentId = department.Id,
            //        FullName = employee.FirstName + " " + employee.LastName,
            //        AnnualSalary = employee.AnnualSalary,
            //        DepartmentName = department.LongName
            //    }).OrderBy(department => department.DepartmentId).ThenByDescending(employee => employee.AnnualSalary);

            //foreach (var result in results)
            //{
            //    Console.WriteLine($"Full Name: {result.FullName}, Annual Salary: {result.AnnualSalary}, Department Name: {result.DepartmentName}");
            //}




            //Console.WriteLine("----------OrderBy using Query Syntax----------");
            //List<Employee> employeeList = Data.GetEmployees();
            //List<Department> departmentList = Data.GetDepartments();

            //var results = from department in departmentList
            //              join employee in employeeList
            //              on department.Id equals employee.DepartmentId
            //              orderby employee.AnnualSalary, employee.FirstName descending
            //              select new
            //              {
            //                  DepartmentId = department.Id,
            //                  FullName = employee.FirstName + " " + employee.LastName,
            //                  AnnualSalary = employee.AnnualSalary,
            //                  DepartmentName = department.LongName
            //              };
            //foreach (var result in results)
            //{
            //    Console.WriteLine($"Full Name: {result.FullName}, Annual Salary: {result.AnnualSalary}, Department Name: {result.DepartmentName}");
            //}




            //Console.WriteLine("----------GroupBy using Method Syntax----------");
            //List<Employee> employeeList = Data.GetEmployees();
            //List<Department> departmentList = Data.GetDepartments();

            //var results = employeeList.GroupBy(employee => employee.DepartmentId);
            //foreach (var empGroup in results)
            //{
            //    Console.WriteLine($"Department Id: {empGroup.Key}"); //empGroup.Key is the key that was used for grouping - employee.DepartmentId
            //    foreach (Employee emp in empGroup)
            //    {
            //        Console.WriteLine($"Employee Full Name: {emp.FirstName} {emp.LastName}");
            //    }
            //}







            //Console.WriteLine("----------GroupBy using Query Syntax----------");
            //List<Employee> employeeList = Data.GetEmployees();
            //List<Department> departmentList = Data.GetDepartments();

            //var results = from employee in employeeList
            //              group employee by employee.DepartmentId;

            //foreach(var empGroup in results)
            //{
            //    Console.WriteLine($"Department Id: {empGroup.Key}");
            //    foreach(Employee emp in empGroup)
            //    {
            //Console.WriteLine($"Full Name: {emp.FirstName} {emp.LastName}, Annual Salary: {emp.AnnualSalary}, Department Id: {emp.DepartmentId}");
            //    }
            //}





            //Console.WriteLine("----------ToLookUp using Method Syntax----------");
            //List<Employee> employeeList = Data.GetEmployees();
            //List<Department> departmentList = Data.GetDepartments();

            //var results = employeeList.OrderBy(x => x.DepartmentId).ToLookup(employee => employee.DepartmentId);
            //foreach (var groupEmp in results)
            //{
            //    Console.WriteLine($"Department Id: {groupEmp.Key}");
            //    foreach(var emp in groupEmp)
            //    {
            //        Console.WriteLine($"Full Name: {emp.FirstName} {emp.LastName}, Annual Salary: {emp.AnnualSalary}, Department Id: {emp.DepartmentId}");
            //    }
            //}



            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();
            
            var annualSalaryCompare = 2000;
            bool isTrueAll =  employeeList.All(emp => emp.AnnualSalary >  annualSalaryCompare);

            if(isTrueAll )
            {
                Console.WriteLine("All employee salaries exceed 2000");
            }
            else
            {
                Console.WriteLine("Not all employee salaries exceed 2000");
            }
        }


    }
}
