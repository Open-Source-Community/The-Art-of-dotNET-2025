// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
//
// namespace SummerTraining_Session1
// {
//     internal class YEILD
//     {
//         static void Main()
//         {
//             foreach (int number in GetNumbers(1, 10))
//             {
//                 Console.WriteLine(number);
//             }
//         }
//
//         static IEnumerable<int> GetNumbers(int start, int end)
//         {
//             for (int i = start; i <= end; i++)
//             {
//
//                 yield return i;
//
//             }
//
//
//         }
//     }
// }