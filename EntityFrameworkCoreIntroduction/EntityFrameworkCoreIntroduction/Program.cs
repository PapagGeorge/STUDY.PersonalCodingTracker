using EntityFrameworkCoreIntroduction.Data;
using EntityFrameworkCoreIntroduction.Models;
using Microsoft.EntityFrameworkCore;



using (var context = new AppDbContext())
{

    //var manager = new Manager()
    //{
    //    MngFirstName = "John",
    //    MngLastName = "Coltrane"
    //};
    //context.Managers.Add(manager);

    //manager = new Manager()
    //{
    //    MngFirstName = "Miles",
    //    MngLastName = "Davis"
    //};
    //context.Managers.Add(manager);

    //context.SaveChanges();

    //var employee = new Employee()
    //{
    //    EmpFirstName = "Mike",
    //    EmpLastName = "Anderson",
    //    Salary = 60000,
    //    ManagerId = 1

    //};
    //context.Employees.Add(employee);


    //employee = new Employee()
    //{
    //    EmpFirstName = "Mark",
    //    EmpLastName = "Jason",
    //    Salary = 40000,
    //    ManagerId = 1

    //};
    //context.Employees.Add(employee);

    //employee = new Employee()
    //{
    //    EmpFirstName = "Bill",
    //    EmpLastName = "Candle",
    //    Salary = 35000,
    //    ManagerId = 2

    //};
    //context.Employees.Add(employee);

    //context.SaveChanges();

    //var EmployeeDetails = new EmployeeDetails()
    //{
    //    Address = "India",
    //    PhoneNumber = "6030862",
    //    Email = "Mike@mail.com",
    //    EmployeeId = 1
    //};
    //context.EmployeeDetails.Add(EmployeeDetails);

    //EmployeeDetails = new EmployeeDetails()
    //{
    //    Address = "Singapore",
    //    PhoneNumber = "6030830",
    //    Email = "Mark@mail.com",
    //    EmployeeId = 2
    //};
    //context.EmployeeDetails.Add(EmployeeDetails);

    //EmployeeDetails = new EmployeeDetails()
    //{
    //    Address = "Greece",
    //    PhoneNumber = "6030855",
    //    Email = "Bill@mail.com",
    //    EmployeeId = 3
    //};
    //context.EmployeeDetails.Add(EmployeeDetails);

    //context.SaveChanges();

    //var project = new Project()
    //{
    //    Name = "Develop"
    //};
    //context.Projects.Add(project);

    //project = new Project()
    //{
    //    Name = "Testing"
    //};
    //context.Projects.Add(project);

    //context.SaveChanges();

    //var empProject = new EmployeeProject()
    //{
    //    EmployeeId = 1,
    //    ProjectId = 1
    //};
    //context.EmployeeProjects.Add(empProject);

    //empProject = new EmployeeProject()
    //{
    //    EmployeeId = 2,
    //    ProjectId = 1
    //};
    //context.EmployeeProjects.Add(empProject);

    //empProject = new EmployeeProject()
    //{
    //    EmployeeId = 3,
    //    ProjectId = 2
    //};
    //context.EmployeeProjects.Add(empProject);

    //context.SaveChanges();


    //Retrieve and display all the Employees
    //Console.WriteLine("-----Display All Employees-----");
    //var employees = context.Employees.ToList();
    //foreach (var emp in employees)
    //{
    //    Console.WriteLine($"Full Name: {emp.EmpFirstName} {emp.EmpLastName}\n");
    //}


    ////Retrieve and display specific Employee
    //Console.WriteLine("-----Display Specific Employee-----");
    //var emp1 = context.Employees.Single(emp => emp.EmployeeId == 2);
    //Console.WriteLine($"Full Name: {emp1.EmpFirstName} {emp1.EmpLastName}\n");

    ////Update Employee
    //Console.WriteLine("-----Updated Specific Employee-----");
    //var emp2 = context.Employees.Single(emp => emp.EmployeeId == 2);
    //emp2.EmpFirstName = "James";
    //context.SaveChanges();
    //Console.WriteLine($"Full Name: {emp2.EmpFirstName} {emp2.EmpLastName}\n");

    ////Delete data from the database
    //Console.WriteLine("-----Display All Employees -----");
    //var emp3 = context.Employees.Single(emp => emp.EmployeeId == 3);
    //context.Employees.Remove(emp3);
    //foreach (var emp in employees)
    //{
    //    Console.WriteLine($"Full Name: {emp.EmpFirstName} {emp.EmpLastName}\n");
    //}


    ////Eager loading is a technique used to load related entities along with the
    ////main entity being queried in a single database round-trip. Use the Include() or
    ////ThenInclude().
    ////In the Include and ThenInclude() method, you specify the navigation properties of the current entity
    ////for which you want to include related entities.
    ////The ThenIncule() allows us to include further related entities.

    //Console.WriteLine("-----Accessing EmployeeDetails property with eager loading-----");
    //var emp4 = context.Employees.Where(emp => emp.EmployeeId == 3).Include(emp => emp.EmployeeDetails)
    //    .FirstOrDefault();

    //Console.WriteLine($"Employee's Address: {emp4.EmployeeDetails.Address}");



    //Console.WriteLine("-----Eager Loading Many to Many relationship-----");
    //var emp5 = context.Projects.Include(ep => ep.EmployeeProjects).ThenInclude(emp => emp.Employee);

    //foreach(var project in emp5)
    //{
    //    Console.WriteLine($"Project Name: {project.Name}");

    //    foreach (var empProj in project.EmployeeProjects)
    //    {
    //        Console.WriteLine($"Employee Id: {empProj.EmployeeId}, Employee Full Name:" +
    //            $" {empProj.Employee.EmpFirstName} {empProj.Employee.EmpLastName}");
    //    }
    //}



    //Console.WriteLine("-----Explicit Loading-----");
    ////Explicit loading is a technique in which related data is explicitly loaded
    ////from the database at a later time.
    ////We can use the Entry method of the DbContext class allong with the Collection or Reference methods.

    //var emp6 = context.Employees.ToList();
    //foreach (var emp in emp6)
    //{
    //    context.Entry(emp).Reference(emp => emp.EmployeeDetails).Load();
    //    Console.WriteLine($"Employee Full Name: {emp.EmpFirstName} {emp.EmpLastName}, " +
    //        $"Address: {emp.EmployeeDetails.Address}");
    //}


    ////Explicit loading - one to many relationship
    //var managers = context.Managers.ToList();

    //foreach(var manager in managers)
    //{
    //    Console.WriteLine($"Manager Name: {manager.MngFirstName} {manager.MngLastName}");
    //    context.Entry(manager).Collection(emp => emp.Employees).Load();
    //    if (manager.Employees.Any())
    //    {
    //        foreach(var emp in manager.Employees)
    //        {
    //            Console.WriteLine($"Employee Full Name: {emp.EmpFirstName} {emp.EmpLastName}");
    //        }
    //    }
    //}




    //In case of Lazy Loading related data is loaded from the database
    //when the navigation property is accessed.

    //Two ways:
    //i)Lazy Loading with proxies
    //ii)Lazy Loading without proxies.
    //We will demonstrate the first way.


    //Install nuget package Microsoft.EntityFrameworkCore.Proxies
    //Use UseLazyLoadingProxies() in OnConfiguring method.
    //Entity framework core will enable lazy loading for any navigation property
    //that is virtual.
    //We need to make all navigation properties virtual.


    var manager2 = context.Managers.ToList();
    foreach (var manager in manager2)
    {
        Console.WriteLine($"Manager Full Name: {manager.MngFirstName} {manager.MngLastName}");

        if (manager.Employees.Any())
        {
            foreach(var employee in manager.Employees)
            {
                Console.WriteLine($"Employee Full Name: {employee.EmpFirstName} {employee.EmpLastName}");
            }
        }
    }



}