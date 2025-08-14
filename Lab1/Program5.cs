using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class Program5
    {
        public void p5()
        {
            Console.Write("Enter Length : ");
            int x = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Breath : ");
            int y = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Radius : ");
            int z = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Square = " + x * x + " Rectangle = " + x * y + " Circle = " + 3.14 * (z * z));
        }
    }
}
