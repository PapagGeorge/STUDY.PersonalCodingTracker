using EntityFrameworkCoreIntroduction.Data;
using EntityFrameworkCoreIntroduction.Models;



using(var context = new AppDbContext())
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
    Console.WriteLine("-----Display All Employees-----");
    var employees = context.Employees.ToList();
    foreach (var emp in employees)
    {
        Console.WriteLine($"Full Name: {emp.EmpFirstName} {emp.EmpLastName}\n");
    }


    //Retrieve and display specific Employee
    Console.WriteLine("-----Display Specific Employee-----");
    var emp1 = context.Employees.Single(emp => emp.EmployeeId == 2);
    Console.WriteLine($"Full Name: {emp1.EmpFirstName} {emp1.EmpLastName}\n");

    //Update Employee
    Console.WriteLine("-----Updated Specific Employee-----");
    var emp2 = context.Employees.Single(emp => emp.EmployeeId == 2);
    emp2.EmpFirstName = "James";
    context.SaveChanges();
    Console.WriteLine($"Full Name: {emp2.EmpFirstName} {emp2.EmpLastName}\n");

    //Delete data from the database
    Console.WriteLine("-----Display All Employees -----");
    var emp3 = context.Employees.Single(emp => emp.EmployeeId == 3);
    context.Employees.Remove(emp3);
    foreach (var emp in employees)
    {
        Console.WriteLine($"Full Name: {emp.EmpFirstName} {emp.EmpLastName}\n");
    }

}