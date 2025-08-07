using efCore;
using efCore.Model.CompanyClasses;
using Microsoft.EntityFrameworkCore;

public class Program
{

    static void Main(string[] args)
    {
        /* General note:
        to avoid Exception in the below code, you should uncomment the lines that Add to db & save changes to the database.
        then run the code.
        then comment the lines again.


        because you can't add the same data to the database twice. with the same Primary key
        */

        #region CRUD



        using (var db = new SchoolContext())
        {

            #region Add
            Department dept1 = new Department
            {
                DepartmentId = 2,
                Name = "CS",
                Description = "Computer Science",
            };
            Department dept2 = new Department
            {
                DepartmentId = 3,
                Name = "IS",
                Description = "Information Science",
            };

           //  db.Departments.AddRange(new List<Department>() { dept1, dept2 });
            //db.SaveChanges(); //To Save Changes to the Database



            //--------------------------------------------------------------
            var res = db.Departments;
            foreach (var i in res)
                Console.WriteLine(i);
            Console.WriteLine("---------------------------");
            //--------------------------------------------------------------



            //Add forigen key without navigation property
            Employee emp1 = new Employee
            {
                EmpSSN = 2,
                Name = "Anas Elwkel",
                Salary = 20000.0m,
                Age = 20,
                Address = "Cairo",
                DepartmentId = 2
            };

            //Add forigen key with navigation property (In case you don't want to use the foreign key property)
            var targetDept = db.Departments.FirstOrDefault(d => d.DepartmentId == 2);
            Employee emp2 = new Employee
            {
                EmpSSN = 3,
                Name = "Poula Saber",
                Salary = 25000.0m,
                Age = 20,
                Address = "Cairo",
                Dept = targetDept
            };

          //  db.Employees.AddRange(new List<Employee>() { emp1, emp2 });
            //db.SaveChanges(); //To Save Changes to the Database

            //Another way to add Student by Department

            //var targetDept2 = db.Departments.First(d => d.DepartmentId == 3);
            //targetDept2.Employees.Add(new Employee()
            //{
            //    EmpSSN = 4,
            //    Name = "Mohamed Zanaty",
            //    Salary = 27000.0m,
            //    Age = 20,
            //    Address = "Cairo",
            //    Dept = (Department)targetDept2
            //});
            //db.SaveChanges();



            //--------------------------------------------------------------
            var res2 = db.Employees;
            foreach (var i in res2)
                Console.WriteLine(i);
            Console.WriteLine("---------------------------");
            //--------------------------------------------------------------
            List<Project> projects = new()
                  {
                      new Project
                      {

                          ProjId = 1,
                          ProjectName = "Course Management System",
                          ProjectDesc = "this is an amazing Course Management System..."
                      },
                      new Project
                      {
                          ProjId = 2,
                          ProjectName = "Egypt Flight Reservation",
                      },
                      new Project
                      {
                          ProjId = 3,
                          ProjectName = "Map Tool",
                          ProjectDesc = "Getting the shortest path"
                      }
                  };
             //      db.Projects.AddRange(projects);
           // db.SaveChanges();
            var res3 = db.Projects;

            //--------------------------------------------------------------

            foreach (var i in res3)
                Console.WriteLine(i);
            Console.WriteLine("---------------------------");

            //--------------------------------------------------------------


            var empPoula = db.Employees.FirstOrDefault(e => e.EmpSSN == 3);
            var empAnas = db.Employees.FirstOrDefault(e => e.EmpSSN == 2);
            var proj1 = db.Projects.FirstOrDefault(p => p.ProjId == 1);
            var proj2 = db.Projects.FirstOrDefault(p => p.ProjId == 2);


            // Assign projects to employees
            if (empAnas != null && proj1 != null && proj2 != null)
            {
                //empAnas.projects.Add(proj1);
               // empAnas.projects.Add(proj2);
            }

            if (empPoula != null && proj2 != null)
            {
             //     empPoula.projects.Add(proj2);
            }

           // db.SaveChanges();






            #endregion

            #region Update
            var proj = db.Projects.FirstOrDefault(p => p.ProjId == 3);
            if (proj != null)
            {
                proj.ProjectDesc = "Powerful Project";

            }
           // db.SaveChanges();
            Console.WriteLine(proj != null ? "Project desc updated" : "Not Found");

            foreach (var i in db.Projects)
                Console.WriteLine(i);
            #endregion

            #region Delete
            var deletedEmployee = db.Employees.FirstOrDefault(emp => emp.EmpSSN == 4);
            if (deletedEmployee != null)
            {
                db.Employees.Remove(deletedEmployee);

               // db.SaveChanges();

                //--------------------------------------------------------------
                foreach (var i in db.Employees)
                    Console.WriteLine(i);

                Console.WriteLine("---------------------------");
                //--------------------------------------------------------------
            }

            #endregion



            #endregion


            #region Loading



            var emp = db.Employees.Where(e => e.EmpSSN == 2).FirstOrDefault();

            Console.WriteLine(emp);

            // ❌ This line will throw a NullReferenceException if loading is not used:
            //Console.WriteLine($"Department Name: {emp.Dept.Name}");




















            #region Eager Loading - Employee + Projects
            Console.WriteLine("\n=== EAGER LOADING: Employees + Project ===");

            var employeesWithSkills = db.Employees
                .Include(e => e.projects)
                .ToList();

            foreach (var employee in employeesWithSkills)
            {
                Console.WriteLine($"Emp: {employee.Name}");
                foreach (var p in employee.projects)
                {
                    Console.WriteLine($"   -> Project: {p.ProjectName}");
                }
            }
            #endregion









            #region Explicit Loading - Employee + Projects
            Console.WriteLine("\n=== EXPLICIT LOADING: One Employee + Project ===");// 1 to many

            var employeeExplicit = db.Employees.FirstOrDefault(e => e.EmpSSN == 2);

            if (employeeExplicit != null)
            {
                db.Entry(employeeExplicit).Collection(e => e.projects).Load(); // Collection is used for one-to-many relationships
                Console.WriteLine($"Emp: {employeeExplicit.Name}");
                foreach (var p in employeeExplicit.projects)
                {
                    Console.WriteLine($"   -> Project: {p.ProjectName}");
                }
            }




            Console.WriteLine("\n=== EXPLICIT LOADING: One Employee + one dept ===");// 1 to 1
            Console.WriteLine("-----------------------------");
            var empWithDept = db.Employees.FirstOrDefault(e => e.EmpSSN == 2);

            if (empWithDept != null)
            {
                db.Entry(empWithDept).Reference(e => e.Dept).Load();// Reference is used for one-to-one relationships

                Console.WriteLine($"Emp -> Dept: {empWithDept.Dept.Name}");

            }
            #endregion





















            #region Hands-on


            // eager: List all doctors and the number of patients they have appointments with. (Use Include and LINQ.)





            // List all doctors and the number of patients they have appointments with(use eager loading).


            #endregion

            #endregion





















        }



    }
}