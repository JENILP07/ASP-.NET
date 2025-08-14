using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    internal class Program8
    {
        public void p8()
        {
            Console.Write("Enter a Number : ");
            int x = Convert.ToInt32(Console.ReadLine());
            int denominator = 0;
            try
            {
                if (x % 2 != 0) { x = x / denominator; }
                else { Console.WriteLine("Even Number"); }
            }
            catch (Exception e)
            {
                Console.WriteLine(e + "                     NOT AN EVEN NUMBER");
            }
        }
    }
}
