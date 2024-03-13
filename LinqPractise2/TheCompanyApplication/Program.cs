using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TCPData;
using TCPExtensions;

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




            //Console.WriteLine("------Single, SingleOrDefault Operataions-----");
            //var result = employeeList.Single(emp => emp.Id == 2);
            //Console.WriteLine($"Full Name {result.FirstName} {result.LastName}");

            //var result2 = departmentList.SingleOrDefault(dep => dep.Id == 8);
            //if(result2 != null)
            //{
            //    Console.WriteLine($"Department unique Id: {result2.LongName}");
            //}
            //else
            //{
            //    Console.WriteLine("there is no department with such an Id");
            //}




            //Console.WriteLine("------SequenceEqual-----");
            //List <int> numbers = new List<int> { 1 ,2 ,3 ,4 ,5 ,6 ,7 ,8 ,9 };
            //List<int> numbers2 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //bool areListstTheSame = numbers.SequenceEqual(numbers2);
            //if(areListstTheSame)
            //{
            //    Console.WriteLine("Number lists are identical");
            //}
            //else
            //{
            //    Console.WriteLine("Number lists are different from each other");
            //}

            //List <Employee> employeesToCompare = Data.GetEmployees();
            //var areEmployeeListsIdentical = employeeList.SequenceEqual(employeesToCompare, new EmployeeComparer());
            //if (areEmployeeListsIdentical)
            //{
            //    Console.WriteLine("Employee Lists are identical");
            //}
            //else
            //{
            //    Console.WriteLine("Employee lists are different from each other");
            //}




            //Console.WriteLine("------Concat Operator-----");
            //List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            //List<int> numbers2 = new List<int> { 6, 7, 8, 9, 10 };

            //var result = numbers.Concat(numbers2);
            //foreach (var number in result)
            //{
            //    Console.WriteLine(number);
            //}
            //Console.WriteLine();

            //List<Employee> empListToConcat = new List<Employee> { new Employee { FirstName = "John", LastName = "Coltrane", Id = 100,
            //IsManager = true, AnnualSalary = 100000, DepartmentId = 100} };

            //var result2 = employeeList.Concat(empListToConcat);
            //foreach(var employee in result2 )
            //{
            //    Console.WriteLine($"Full Name: {employee.FirstName} {employee.LastName}");
            //}




            //Console.WriteLine("------Aggregate Operator-----");
            //Let's say we want to find the total annual salaries of employees plus their bonuses
            //that depend on if isManager is true or false.
            //decimal totalAnnualSalary = employeeList.Aggregate<Employee, decimal>(0, (totAnnualSalary, emp) =>
            //{
            //    decimal bonus = (emp.IsManager) ? 0.04m : 0.02m;
            //    totAnnualSalary = (emp.AnnualSalary + (emp.AnnualSalary * bonus)) + totAnnualSalary;
            //    return totAnnualSalary;
            //});

            //Console.WriteLine($"Total annual salaries of all employees (including bonus): {totalAnnualSalary}");

            //Let's say we want to write for each employee their full name and their annual salaries including bonus.
            //string data = employeeList.Aggregate<Employee, string, string>("Employee annual salary (including bonus): ", (message, emp) =>
            //{
            //    decimal bonus = (emp.IsManager) ? 0.04m : 0.02m;
            //    message += $"{emp.FirstName} {emp.LastName} - {emp.AnnualSalary + (emp.AnnualSalary * bonus)}, ";
            //    return message;
            //}, message => message.Substring(0, message.Length -2));
            //Console.WriteLine(data);
            //Console.ReadKey();






            //Console.WriteLine("------Average Operator-----");
            //var averageSalary = employeeList.Where(emp =>emp.DepartmentId == 2).Average(emp => emp.AnnualSalary);
            //Console.WriteLine($"Average Salary ()Technology department: {averageSalary}");





            //Console.WriteLine("------Count Operator-----");
            //var count = employeeList.Count(emp => emp.DepartmentId == 2);
            //Console.WriteLine($"Number of employees in Tecnology Department: {count}");





            //Console.WriteLine("------Sum Operator-----");
            //var annualSalariesSum = employeeList.Sum(emp => emp.AnnualSalary);
            //Console.WriteLine($"Sum of all annual salaries: {annualSalariesSum}");




            //Console.WriteLine("------Max Operator-----");
            //var maxSalary = employeeList.Max(emp => emp.AnnualSalary);
            //Console.WriteLine($"Employees Max Salary: {maxSalary}");




            //Console.WriteLine("------DefaultIfEmpty Operator-----");
            //List <Employee> empList = new List<Employee>();
            //empList.DefaultIfEmpty(new Employee { Id = 0 }); //Default value if the list is empty

            //var result = empList.ElementAt(0);
            //if(result.Id == 0)
            //{
            //    Console.WriteLine("The list is empty");
            //}
            //Console.WriteLine();




            //Console.WriteLine("------Empty Operator-----");
            //List<Employee> empList = Enumerable.Empty<Employee>().ToList();
            //empList.Add(new Employee { Id = 1, FirstName = "Mike", LastName = "Johnson" });
            //foreach (var item in empList)
            //{
            //    Console.WriteLine($"{item.FirstName} {item.LastName}");
            //}




            //Console.WriteLine("------Range Operator-----");
            //var intCollection = Enumerable.Range(10, 5);
            //foreach(var number in intCollection)
            //{
            //    Console.WriteLine(number);
            //}




            //Console.WriteLine("------Repeat Operator-----");
            //var stringRepeated = Enumerable.Repeat<string>("Repeat message", 10).ToList();
            //foreach (var message in stringRepeated)
            //{
            //    Console.WriteLine(message);
            //}




            //Console.WriteLine("------Distinct Operator-----");
            //List<int> numbers = new List<int> { 1, 2, 2, 3, 4, 5, 5, 7, 14, 16, 16, 19, 19, 25, 33, 36, 39, 39, 39 };
            //var distinctNumbers = numbers.Distinct();
            //foreach(int number in distinctNumbers)
            //{
            //    Console.WriteLine(number);
            //}




            //Console.WriteLine("------Except Operator-----");   ////This will keep the elements that are present in the first collection only
            //IEnumerable<int> numberCollection1 = new List<int> { 1, 2, 3, 4, 5, 6 };
            //IEnumerable<int> numberCollection2 = new List<int> { 3, 4, 5, 6, 7, 8 };
            //IEnumerable result = numberCollection1.Except(numberCollection2);

            //foreach (int number in result)
            //{
            //    Console.WriteLine(number);
            //}

            //List<Employee> newListEmployee = new List<Employee>();
            //Employee employee = new Employee
            //{
            //    Id = 1,
            //    FirstName = "Bob",
            //    LastName = "Jones",
            //    AnnualSalary = 60000.3m,
            //    IsManager = true,
            //    DepartmentId = 1
            //};
            //newListEmployee.Add(employee);
            //employee = new Employee
            //{
            //    Id = 2,
            //    FirstName = "Sarah",
            //    LastName = "Jameson",
            //    AnnualSalary = 80000.1m,
            //    IsManager = true,
            //    DepartmentId = 2
            //};
            //newListEmployee.Add(employee);
            //employee = new Employee
            //{
            //    Id = 3,
            //    FirstName = "Mike",
            //    LastName = "Johnson",
            //    AnnualSalary = 50000.2m,
            //    IsManager = false,
            //    DepartmentId = 2
            //};
            //newListEmployee.Add(employee);
            //employee = new Employee
            //{
            //    Id = 4,
            //    FirstName = "Marry",
            //    LastName = "Jane",
            //    AnnualSalary = 20000.2m,
            //    IsManager = false,
            //    DepartmentId = 3
            //};
            //newListEmployee.Add(employee);

            //var newEmployees = newListEmployee.Except(employeeList, new EmployeeComparer());
            //foreach (var emp in newEmployees)
            //{
            //    Console.WriteLine($"Full Name {emp.FirstName} {emp.LastName}");
            //}




            //Console.WriteLine("------Intersect Operator-----"); ////This will return the elements that exist in both collections
            //List<Employee> newListEmployee = new List<Employee>();
            //Employee employee = new Employee
            //{
            //    Id = 1,
            //    FirstName = "Bob",
            //    LastName = "Jones",
            //    AnnualSalary = 60000.3m,
            //    IsManager = true,
            //    DepartmentId = 1
            //};
            //newListEmployee.Add(employee);
            //employee = new Employee
            //{
            //    Id = 2,
            //    FirstName = "Sarah",
            //    LastName = "Jameson",
            //    AnnualSalary = 80000.1m,
            //    IsManager = true,
            //    DepartmentId = 2
            //};
            //newListEmployee.Add(employee);
            //employee = new Employee
            //{
            //    Id = 3,
            //    FirstName = "Mike",
            //    LastName = "Johnson",
            //    AnnualSalary = 50000.2m,
            //    IsManager = false,
            //    DepartmentId = 2
            //};
            //newListEmployee.Add(employee);
            //employee = new Employee
            //{
            //    Id = 4,
            //    FirstName = "Marry",
            //    LastName = "Jane",
            //    AnnualSalary = 20000.2m,
            //    IsManager = false,
            //    DepartmentId = 3
            //};
            //newListEmployee.Add(employee);

            //var commonElementsEmployeeList = newListEmployee.Intersect(employeeList, new EmployeeComparer());
            //foreach (var emp in commonElementsEmployeeList)
            //{
            //    Console.WriteLine($"Full Name: {emp.FirstName} {emp.LastName}");
            //}




            //Console.WriteLine("------Union Operator-----"); //returns distinct elements from both collections
            //List<Employee> newListEmployee = new List<Employee>();
            //Employee employee = new Employee
            //{
            //    Id = 1,
            //    FirstName = "Bob",
            //    LastName = "Jones",
            //    AnnualSalary = 60000.3m,
            //    IsManager = true,
            //    DepartmentId = 1
            //};
            //newListEmployee.Add(employee);
            //employee = new Employee
            //{
            //    Id = 2,
            //    FirstName = "Sarah",
            //    LastName = "Jameson",
            //    AnnualSalary = 80000.1m,
            //    IsManager = true,
            //    DepartmentId = 2
            //};
            //newListEmployee.Add(employee);
            //employee = new Employee
            //{
            //    Id = 3,
            //    FirstName = "Mike",
            //    LastName = "Johnson",
            //    AnnualSalary = 50000.2m,
            //    IsManager = false,
            //    DepartmentId = 2
            //};
            //newListEmployee.Add(employee);
            //employee = new Employee
            //{
            //    Id = 4,
            //    FirstName = "Marry",
            //    LastName = "Jane",
            //    AnnualSalary = 20000.2m,
            //    IsManager = false,
            //    DepartmentId = 3
            //};
            //newListEmployee.Add(employee);

            //var distinctElementsCollection = newListEmployee.Union(employeeList, new EmployeeComparer());
            //foreach (var emp in distinctElementsCollection)
            //{
            //    Console.WriteLine($"Full Name: {emp.FirstName} {emp.LastName}");
            //}



            //Console.WriteLine("------Skip Operator-----"); //skip a number of elements and return the remainder
            //var result = employeeList.Skip(2);
            //foreach (var emp in result)
            //{
            //    Console.WriteLine($"Full Name: {emp.FirstName} {emp.LastName}");
            //}




            //Console.WriteLine("------SkipWhile Operator-----"); //used to bypass elements in a sequence until a condition is no longer met
            //employeeList.Add(new Employee { Id = 5, FirstName = "John", LastName = "Coltrane", IsManager = true, AnnualSalary = 1000});
            ////the new list element is included regardless the condition of the SkipWhile()
            //var result = employeeList.SkipWhile(emp => emp.AnnualSalary > 50000);
            //foreach (var emp in result)
            //{
            //    Console.WriteLine($"Full Name: {emp.FirstName} {emp.LastName} Id: {emp.Id}");
            //}



            //Console.WriteLine("------Take Operator-----"); //the Take operator is used to return a specified number of elements from the start of a sequence.
            //                                               //It retrieves the first n elements from a sequence and discards the rest.
            //var result = employeeList.Take(2);
            //foreach (var emp in result)
            //{
            //    Console.WriteLine($"Full Name: {emp.FirstName} {emp.LastName} Id: {emp.Id}");
            //}



            //Console.WriteLine("------TakeWhile-----"); //The TakeWhile operator is used to return elements from a sequence until a specified condition is no longer met
            //var result = employeeList.TakeWhile(emp => emp.AnnualSalary > 50000);
            //foreach (var emp in result)
            //{
            //    Console.WriteLine($"Full Name: {emp.FirstName} {emp.LastName} Id: {emp.Id}");
            //}



            //Console.WriteLine("------ToList()-----");//The ToList() method in LINQ is used to convert an IEnumerable<T> sequence into a List<T>.
            //                                         //It materializes the sequence into a concrete list object.

            //List<Employee> empList = (from emp in employeeList
            //                         where emp.IsManager == true
            //                         select emp).ToList();
            //foreach (Employee emp in empList)
            //{
            //    Console.WriteLine($"Full Name: {emp.FirstName} {emp.LastName} Id: {emp.Id}");
            //}




            //Console.WriteLine("------ToDictionary()-----");
            //Dictionary<int, Employee> employeeDict = (from emp in employeeList
            //                                          where emp.AnnualSalary > 50000
            //                                          select emp).ToDictionary<Employee, int>(emp => emp.Id);

            //foreach (KeyValuePair<int, Employee> emp in employeeDict)
            //{
            //    int id = emp.Key;
            //    Employee employee = emp.Value;
            //    Console.WriteLine($"Full Name: {employee.FirstName} {employee.LastName} Id: {id}");
            //}




            //Console.WriteLine("------ToArray()-----");
            //Employee[] results = (from emp in employeeList
            //                     where emp.AnnualSalary > 50000
            //                     select emp).ToArray();

            //foreach (Employee emp in results)
            //{
            //    Console.WriteLine($"Full Name: {emp.FirstName} {emp.LastName} Id: {emp.Id}");
            //}
        }







    }
    

}

