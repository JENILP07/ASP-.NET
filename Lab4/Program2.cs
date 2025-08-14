using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class Program2
    {
        public void Area(int l) { Console.WriteLine($"Area of Square : {l * l}"); }
        public void Area(int l,int b) { Console.WriteLine($"Area of Rectangle : {l * b}"); }

        public void p2() { Area(5); Area(10,5); }
    }
}
