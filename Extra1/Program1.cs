using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extra1
{
    internal class Program1
    {
        public void P1()
        {
            Console.Write("Enter Lower Bound: ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Upper Bound: ");
            int b = Convert.ToInt32(Console.ReadLine());

            for (int i = Math.Max(2, a); i <= b; i++) 
            {
                bool isPrime = true;

                for (int j = 2; j <= Math.Sqrt(i); j++) 
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}
