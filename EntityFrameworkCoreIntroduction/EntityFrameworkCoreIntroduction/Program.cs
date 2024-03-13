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
    //context.SaveChanges();

    var employee = new Employee()
    {
        EmpFirstName = "Mike",
        EmpLastName = "Anderson",
        Salary = 60000,
        ManagerId = 1

    };
    context.Employees.Add(employee);
    context.SaveChanges();
}