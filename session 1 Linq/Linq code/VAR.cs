using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SummerTraining_Session1
{
    internal class VAR
    {
        public static void Main(string[] args)
        {


            //================    1. When the type is obvious    ==========


            // var age = 21;          // int
            // var name = "Mahmoud";  // string
            // var isActive = true;   // bool
            // var price = 99.99;     // double
            // var letter = 'A';      // char




            // Console.WriteLine(age);
            // Console.WriteLine(name);
            // Console.WriteLine(isActive);
            // Console.WriteLine(price);
            // Console.WriteLine(letter);
            //
            //
            // Console.WriteLine(age.GetType());
            // Console.WriteLine(name.GetType());
            // Console.WriteLine(isActive.GetType());
            // Console.WriteLine(price.GetType());
            // Console.WriteLine(letter.GetType());


            //============== 2. When the type is long or complex    ==========

            List<int> num = new List<int> { 1, 2, 3, 4, 5 };
            var num = new List<int> { 1, 2, 3, 4, 5 };
            



            //Dictionary<string, int> ages = new Dictionary<string, int>
            //{
            //    { "Alice", 30 },
            //    { "Bob", 25 }
            //};
            
            //var ages = new Dictionary<string, int>
            //{
            //    { "Alice", 30 },
            //    { "Bob", 25 }
            //};



            //Dictionary<string, List<Tuple<int, string>>>() students =
            //     new Dictionary<string, List<Tuple<int, string>>>();
            
            
            // //var students =
            //     new Dictionary<string, List<Tuple<int, string>>>();
            
            




            //============ 3. Anonymous Type ==============

            //Person p1 = new Person(1234, "Ali", 22, "01234567890", "mahmoud36hossam@gmail.com");


            //var person = new
            //{
            //    id = 1234,
            //    Name = "Ali",
            //    Age = 22,
            //    phoneNO = "01234567890",
            //    email = "mahmoud36hossam@gmail.com"
            //};







            // var id = 1234;
            // var Name = "Ali";
            // var Age = 22;
            // var phoneNO = "01234567890";
            // var email = "mahmoud36hossam@gmail.com";
            //
            // var person = new
            // {
            //     id,
            //     Name,
            //     Age,
            //     phoneNO,
            //     email
            // };




            //============ 4. LINQ Query ==============


            // List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
            // var evenNumbers = numbers.Where(n => n % 2 == 0);

            // foreach (var even in evenNumbers)
            // {
            //     Console.WriteLine(even);
            // }




        }
        public class Person
        {
            int id;
            string Name;
            int Age;
            string phoneNO;
            string email;

            public Person(int id, string name, int age, string phoneNO, string email)
            {
                this.id = id;
                Name = name;
                Age = age;
                this.phoneNO = phoneNO;
                this.email = email;
            }
        };
    }

}
