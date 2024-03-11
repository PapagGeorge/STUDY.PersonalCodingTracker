﻿using System.Collections;
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




            Console.WriteLine("------OfType filter operation-----");
            ArrayList mixedCollection = Data.GetHeterogeneousDataCollection();

            var stringResult = from item in mixedCollection.OfType<string>()
                               select item;

            foreach( var item in stringResult )
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();


            var intResult = from item in mixedCollection.OfType<int>()
                            select item;
            foreach(var item in intResult )
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            var employeeResult = from employee in mixedCollection.OfType<Employee>()
                                 select employee;
            foreach( var employee in employeeResult )
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName}");
            }
            Console.WriteLine();


        }

        
    }
    

}

