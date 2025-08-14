using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Program1
    {
        public void p1()
        {
            int numerator = 10;
            int denominator = 0;
            try
            {
                int ans = numerator / denominator;
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Exception Cought");
            }
           
        }
    }
}
