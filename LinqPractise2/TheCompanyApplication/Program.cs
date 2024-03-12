using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TCPData;

namespace TheCompanyApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();

            //Console.WriteLine("------All() operation-----");
            //var salaryToCompare = 20000;
            //bool areAllSalariesGreater = employeeList.All(employee => employee.AnnualSalary > salaryToCompare);

            //if(areAllSalariesGreater)
            //{
            //    Console.WriteLine("All salaries are greater than 20000");
            //}
            //else
            //{
            //    Console.WriteLine("At least one salary in not greater than 20000");
            //}




            //Console.WriteLine("------Using Any() operation-----");
            //var salaryToCompare = 20000;
            //bool isAnySalaryGreater = employeeList.Any(employee => employee.AnnualSalary > salaryToCompare);

            //if(isAnySalaryGreater)
            //{
            //    Console.WriteLine("At least one salary is greater than 20000");
            //}
            //else
            //{
            //    Console.WriteLine("No Salary is greater than 20000");
            //}




            //Console.WriteLine("------OfType filter operation-----");
            //ArrayList mixedCollection = Data.GetHeterogeneousDataCollection();

            //var stringResult = from item in mixedCollection.OfType<string>()
            //                   select item;

            //foreach( var item in stringResult )
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine();


            //var intResult = from item in mixedCollection.OfType<int>()
            //                select item;
            //foreach(var item in intResult )
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine();

            //var employeeResult = from employee in mixedCollection.OfType<Employee>()
            //                     select employee;
            //foreach( var employee in employeeResult )
            //{
            //    Console.WriteLine($"{employee.FirstName} {employee.LastName}");
            //}
            //Console.WriteLine();




            //Console.WriteLine("------ElementAt and ElementAtOrDefault-----");
            //var result = employeeList.ElementAt(2);
            //Console.WriteLine($"Full Name: {result.FirstName} {result.LastName}");

            //var result2 = employeeList.ElementAtOrDefault(9);
            //if (result2 != null )
            //{
            //    Console.WriteLine($"Full Name: {result2.FirstName} {result2.LastName}");
            //}
            //else
            //{
            //    Console.WriteLine("There is no element at this position");
            //}
            //Console.WriteLine();





            //Console.WriteLine("------First, FirstOrDefault, Last, LastOrDefault Operatiors-----");
            //string nameToSearch = "Mike";

            //var result = employeeList.First(emp => emp.FirstName.ToLower() == "bob");
            //Console.WriteLine($"Full Name: {result.FirstName} {result.LastName}");

            //var result2 = employeeList.FirstOrDefault(emp => emp.FirstName == nameToSearch);
            //if (result2 != null)
            //{
            //    Console.WriteLine($"Full Name {result2.FirstName} {result2.LastName}");
            //}
            //else
            //{
            //    Console.WriteLine($"There is not an employee with a name {nameToSearch}");
            //}

            //var result3 = employeeList.Last(emp => emp.Id == 2);
            //Console.WriteLine($"Full Name: {result3.FirstName} {result3.LastName}");

            //var result4 = employeeList.LastOrDefault(emp => emp.FirstName == "John");
            //if (result4 != null)
            //{
            //    Console.WriteLine($"Full Name {result4.FirstName} {result4.LastName}");
            //}
            //else
            //{
            //    Console.WriteLine("there is not such a name in the list");
            //}




            Console.WriteLine("------Single, SingleOrDefault Operataions-----");
            var result = employeeList.Single(emp => emp.Id == 2);
            Console.WriteLine($"Full Name {result.FirstName} {result.LastName}");

            var result2 = departmentList.SingleOrDefault(dep => dep.Id == 8);
            if(result2 != null)
            {
                Console.WriteLine($"Department unique Id: {result2.LongName}");
            }
            else
            {
                Console.WriteLine("there is no department with such an Id");
            }

        }

        
    }
    

}

