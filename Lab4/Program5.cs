using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class Program5
    {
        public void Area(int l) { Console.WriteLine($"Area of Square : {l * l}"); }

        public void Area(int l, int b) { Console.WriteLine($"Area of Rectangle : {l * b}"); }

        public void Area(double r) { Console.WriteLine($"Area of Circle : {3.14*(r*r)}"); }

        public void p5() { Area(5); Area(10, 5); Area((double)5); }
    }
}
