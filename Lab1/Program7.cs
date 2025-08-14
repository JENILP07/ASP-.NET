using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Program7
    {
        public void p7()
        {
            Console.Write("Enter Principle amount : ");
            int p = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Rate : ");
            int r = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Time : ");
            int t = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine((p * r * t) / 100);
        }
    }
}
