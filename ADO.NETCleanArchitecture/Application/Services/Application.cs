﻿using Application.Interfaces;
using Domain.Entities;
using Infrastrutcture.Interfaces;
using System.Collections.Generic;

namespace Application.Services
{
    public class Application : IApplication
    {

        private readonly IStudentRepository _studentRepository;
        private readonly IEnumerable<Student> _students = new List<Student>
        {
            new Student(){Name = "John", Age=29, IsCool=true},
            new Student(){Name = "Miles", Age=24, IsCool=false},
            new Student(){Name = "Bill", Age=28, IsCool=true},
            new Student(){Name = "Cannonball", Age=27, IsCool=false},
            new Student(){Name = "Wynton", Age=29, IsCool=true},
            new Student(){Name = "Paul", Age=22, IsCool=false},
            new Student(){Name = "Jimmy", Age=23, IsCool=true}
        };

        public Application (IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public void BulkInsertTextRun()
        {
            if(_students is null || !_students.Any())
            {
                throw new Exception("Students should not be null or empty");
            }
            _studentRepository.BulkInsertStudentsWithProcedure(_students);
        }

        public void Run()
        {
            IEnumerable <Student> _students = _studentRepository.GetCoolStudentsWithProcedure();
            if (_students is null || !_students.Any())
            {
                throw new Exception("Students should not be null or empty");
            }
            foreach (var student in _students)
            {
                Console.WriteLine($"Name: {student.Name}  Age:{student.Age}  IsCool:{student.IsCool}");
            }
            
        }

        public void Stop()
        {
            Student _student = new Student()
            {
                Name = "Thomas",
                Age = 27,
                IsCool=false
            };
            _studentRepository.InsertStudentWithProcedure(_student);
            
        }
    }
}
