using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program1
    {
        public void P1()
        {
            Console.Write("Enter Array Length : ");
            int n = Convert.ToInt32(Console.ReadLine());
            int[] arr = new int[n];

            Console.WriteLine("Enter Array Elements ");
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Elements {i+1} =  ");
                arr[i] = Convert.ToInt32(Console.ReadLine());
            }
            int max = 0;
            int min = arr[0];

            for (int i = 0; i < n; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                }
                if (arr[i] < min)
                {
                    min = arr[i];
                }
            }        

            Console.WriteLine(max);
            Console.WriteLine(min);
        }
    }
}
