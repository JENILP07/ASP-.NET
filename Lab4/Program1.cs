using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class Program1
    {   
        public void Sum(int a, int b) { Console.WriteLine(a + b); }
        public void Sum(double a, double b) {  Console.WriteLine(a + b); }

        public void p1()
        {
            Console.Write("Int Method : ");
            Sum(10, 20);
            Console.Write("Double Method : ");
            Sum((double)10.20, (double)20.979);
            
        }
    }
}
