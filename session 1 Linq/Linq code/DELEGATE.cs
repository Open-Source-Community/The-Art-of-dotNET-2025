// using System;
// using System.Collections.Generic;
// using System.Diagnostics.CodeAnalysis;
// using System.Linq;
// using System.Security.Cryptography;
// using System.Text;
// using System.Threading.Tasks;
// namespace SummerTraining_Session1
// {
//     public static class DELEGATE
//     {
//         //1 make the problem                    IEnumerable
//         //2 delegate From scratch
//         //3 Replacement delegate by .net predicate
//         //4 make it by template
//         //5 what is linq  
//         // public delegate bool Filter(int item);
//         
//         
//         
//         public static void Main(string[] args)
//         {
//
//
//             List<int> numbers = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
//             var result = getNums(numbers);
//             
//             // foreach (var item in result)
//             // {
//             //     Console.WriteLine(item);
//             //
//             // }
//         }
//         
//
//         public static IEnumerable<int> getNums(List<int> nums)
//         {
//             foreach (var n in nums)
//             {
//                 if (n>5) yield return n;
//             }
//         }
//
//
//
//
//     }
// }
//
//
//
