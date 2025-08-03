// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Text.RegularExpressions;
// using System.Threading.Tasks;
// using SummerTraining_Session1.CLASSES;
//
// namespace SummerTraining_Session1
// {
//     public class LINQ
//     {
//
//         public static void Main(string[] args)
//         {
//             Context context = new Context();
//
//
//
//             #region Query Syntax 
//
//             var result1 = from e in context.employees
//                                                 where e.Department == "Marketing"
//                                               select e;
//
//             foreach (var r in result1)
//             {
//                 Console.WriteLine(r.Name);
//             }
//
//
//             #endregion
//
//             #region Method Syntax
//
//
//
//             //var result2 = context.employees.Where(e =>e.Department == "Marketing");
//             //Console.WriteLine(result2.GetType());
//
//             //foreach (var r in result2)
//             //{
//             //    Console.WriteLine($"{r.Name} , {r.Department}");
//             //}
//
//             #endregion
//
//             #region Select (projection)
//
//
//             //var result = context.employees.Select(e => e.Department);
//             //Console.WriteLine(result);
//             //foreach (var e in result)
//             //{
//             //    Console.WriteLine(e);
//             //}
//
//
//
//             //var result2 = context.employees.Select(e => new { e.Department, e.Name });
//             //foreach (var e in result2)
//             //{
//             //    Console.WriteLine(e);
//             //}
//
//
//
//             ////try to exchange between where and select
//             //var result3 = context.employees.Where(e => e.Id <= 5).Select(e => new { e.Name, e.Department });
//             //foreach (var e in result3)
//             //{
//             //    Console.WriteLine(e);
//             //}
//
//             #endregion
//
//             #region Distinct & DistinctBy
//
//             //It works only with the normal data types 
//             // var result = context.employees.Distinct();
//             //
//             // foreach (var i in result)
//             // {
//             //     Console.WriteLine(i.Name +" " +i.Experience);
//             // }
//             //
//             // Console.WriteLine("+++++++++++++");
//             //
//             // //Get Employees With Distinct Experiences
//             // result = context.employees.DistinctBy(e => e.Experience );
//             //
//             // foreach (var i in result)
//             // {
//             //     Console.WriteLine(i.Name + " " + i.Experience + " " + i.Department);
//             // }
//             
//
//             #endregion
//
//             #region OrderBy
//
//             ////OrderBy arrange Ascendingly by default
//             //var result = context.employees.OrderBy(e => e.Department);
//             //foreach (var i in result)
//             //{
//             //    Console.WriteLine($"{i.Department} , {i.Experience} , {i.Id}");
//             //}
//             //Console.WriteLine("==========================");
//
//
//
//             ////ThenBy arrange Ascendingly by default
//             //var result1 = context.employees.OrderBy(e => e.Department).ThenBy(e => e.Experience).ThenBy(e => e.Id);
//             //foreach (var i in result1)
//             //{
//             //    Console.WriteLine($"{i.Department} , {i.Experience} , {i.Id}");
//             //}
//
//
//
//             //Console.WriteLine("==========================");
//             //var result2 = context.employees.OrderByDescending(e => e.Department).ThenByDescending(e => e.Experience).ThenByDescending(e => e.Id);
//             //foreach (var i in result2)
//             //{
//             //    Console.WriteLine($"{i.Department} , {i.Experience} , {i.Id}");
//             //}
//
//             #endregion
//
//             #region Single Element Operators - Imidiate Execution
//
//             //try to change the operator "<" by ">"
//             // var result = context.employees.First(e => e.Salary < 7000000);
//             // var result2 = context.employees.Last(e => e.Salary < 7000000);
//             //
//             // Console.WriteLine(result.Name);
//             // Console.WriteLine(result2.Name);
//
//
//
//
//             //try to change the operator ">" by "<"
//             //var result3 = context.employees.FirstOrDefault(e => e.Salary < 1000000);
//             //var result4 = context.employees.LastOrDefault(e => e.Salary < 1000000);
//
//             //Console.WriteLine(result3.Name);
//             //Console.WriteLine(result4.Name);
//
//
//
//
//
//
//             //throw Exception if more than 1 match the condition or return default if not found
//             //var result5 = context.employees.SingleOrDefault(e => e.Name == "Bob");
//             //Console.WriteLine(result5.Name);
//
//             #endregion
//
//             #region Aggregate - Imidiate Execution
//             //Count, Max, Min, Sum...
//
//             //var result = context.employees.Max(x => x.Experience);
//             //var result1 = context.employees.Min(x => x.Experience);
//             //Console.WriteLine(result);
//             //Console.WriteLine(result1);
//
//
//
//             //var result2 = context.employees.Count();
//             //Console.WriteLine(result2);
//
//
//
//             //Select Sum of Salaries of The First 5 Employees
//             //var result3 = context.employees.Where(e => e.Id <= 5).Sum(e => e.Salary);
//             //Console.WriteLine(result3);
//
//             #endregion
//
//             #region Take/Skip
//             //Select the First 4 Employees
//             // var result = context.employees.Where(e=>e.Salary>2000 ).Take(4);
//             // foreach (var e in result)
//             // {
//             //     Console.WriteLine(e.Name + " " + e.Salary);
//             // }
//             //
//             // Console.WriteLine("=======================");
//             //
//             // //Select The Employees Without the first 3 salaries
//             // var result1 = context.employees.Skip(3);
//             // foreach (var e in result1)
//             // {
//             //     Console.WriteLine(e.Name + " " + e.Salary);
//             // }
//             #endregion
//
//             #region Generator Operators
//             ////Range 
//             // var result =Enumerable.Range(1, 10);
//             // foreach (var i in result)
//             // {
//             //     Console.WriteLine(i);
//             // }
//
//
//
//             //Empty it make empty sequence
//             //var result1 = Enumerable.Empty<string>();
//
//
//
//             //Repeat      try (result) 
//             // var result2 = Enumerable.Repeat(122, 4);
//             //     foreach (var item in result2)
//             //     {
//             //         Console.WriteLine(item);
//             //     }
//
//
//             #endregion
//
//             #region Select Many
//
//             //var result1 = context.directors.SelectMany(e => e.name.Split(' '));
//             //foreach (var e in result1)
//             //{
//             //    Console.WriteLine(e);
//             //}
//             
//             
//
//             //var result2 = context.directors.SelectMany(e => e.name.ToCharArray());
//             //foreach (var e in result2)
//             //{
//             //         Console.WriteLine(e); 
//             //}
//
//
//             #endregion
//
//             #region Casting Operators - Imidiate Execution
//
//             //var result1 = context.employees.Where(e => e.Salary > 70000);
//             //Console.WriteLine(result1);
//
//             //var result2 = context.employees.Where(e => e.Salary > 70000).ToList();
//             //Console.WriteLine(result2);
//
//             //var result3 = context.employees.Where(e => e.Salary > 70000).ToArray();
//             //Console.WriteLine(result3);
//
//             //var result4 = context.employees.Where(e => e.Salary > 70000).ToDictionary(e => e.Id);
//             //Console.WriteLine(result4);
//
//             #endregion
//
//             #region Quantifiers
//             //var result1 = context.employees.Any(x => x.Salary>10000);
//             //Console.WriteLine(result1);
//
//
//
//             //var result2 = context.employees.All(e => e.Salary > 55000);
//             //Console.WriteLine(result2);
//
//
//             #endregion
//
//             #region Group By
//
//             //GroupBy return asequence of group contain key and elements
//
//             //var result = context.employees.GroupBy(e => e.Department); //key is Department
//             //foreach (var item in result)
//             //    foreach (var i in item)
//             //    {
//             //        Console.WriteLine($"{i.Name} , {i.Department} , {i.Salary}");
//             //    }
//             //}
//
//             #endregion
//
//             #region Inner Join
//
//             // the tareget is to make a table contain Movie Title , Director Name and Nationality of Director
//
//             //var MovieWithDirector = context.movies
//             //    .Join(
//             //    context.directors,
//             //    m => m.directorId,
//             //    d => d.id,
//             //    (m, d) => new
//             //    {
//             //        MovieTitle = m.title,
//             //        DirectorName = d.name,
//             //        NationalityId = d.nationalityId
//
//             //    });
//
//             //Console.WriteLine(MovieWithDirector);
//
//             //foreach (var item in MovieWithDirector)
//             //{
//             //    Console.WriteLine(item);
//             //}
//
//
//
//             ////Select Movie Title With Director Name and Add Nationality of Director
//             //var result = MovieWithDirector
//             //    .Join(
//             //    context.nationalities,
//             //    m => m.NationalityId,
//             //    n => n.id,
//             //    (m, n) => new
//             //    {
//             //        m.MovieTitle,
//             //        m.DirectorName,
//             //        Nationality = n.name
//             //    });
//
//             //foreach (var item in result)
//             //{
//             //    Console.WriteLine(item);
//             //}
//
//             #endregion
//
//             #region Left Join
//
//             //var result = context.movies
//             //    .Join(
//             //    context.directors,
//             //    mov => mov.directorId,
//             //    dir => dir.id,
//             //    (mov, dir) => new
//             //    {
//             //        MovieTitle = mov.title,
//             //        DirectorName = dir.name,
//             //        NationalityId = dir.nationalityId,
//             //    }).GroupJoin(
//             //    context.nationalities,
//             //    MovieWithDirector => MovieWithDirector.NationalityId,
//             //    nation => nation.id,
//             //    (MovieWithDirector, nation) => new
//             //    {
//             //        Movie = MovieWithDirector,
//             //        Nationality = nation
//             //    })                                   // group by return sequence of 2 groups(Movie , Nationality)
//             //    .SelectMany(b => b.Nationality.DefaultIfEmpty(),
//             //    (b, n) => new
//             //    {
//             //        b.Movie,
//             //        Nationality = n
//             //    });
//
//             //foreach (var b in result)
//             //{
//             //    Console.WriteLine(b.Movie + " " + b.Nationality.name);
//             //}
//
//             #endregion
//
//         }
//     }
// }
//
