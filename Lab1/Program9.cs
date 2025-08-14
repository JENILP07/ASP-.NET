using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Program9
    {
        public void p9()
        {
            Console.Write("Enter x : ");
            double x = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter y : ");
            double y = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("X = " + x + " Y = " + y);
            //x = x + y;
            //y = x - y;
            //x = x - y;
            x = x * y;
            y = x / y;
            x = x / y;
            Console.WriteLine("X = " + x + " Y = " + y);
        }
    }
}
