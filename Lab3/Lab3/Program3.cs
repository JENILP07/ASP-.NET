using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Program3
    {
        abstract class Sum
        {

            public abstract void SumOfTow(int a, int b);
            public abstract void SumOfThree(int a, int b, int c);
        }
        class Calculate : Sum
        {
            public override void SumOfTow(int a, int b)
            {
                Console.WriteLine("Sum of Two");
                Console.WriteLine(a + b);
            }
            public override void SumOfThree(int a, int b, int c)
            {
                {
                    Console.WriteLine("Sum of Three");
                    Console.WriteLine(a + b + c); ;
                }
            }
            
        }

        public void p3()
        {
            Console.Write("Enter a : ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter b : ");
            int b = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter c : ");
            int c = Convert.ToInt32(Console.ReadLine());
            Calculate x1 = new Calculate();
            x1.SumOfThree(a,b,c);
            x1.SumOfTow(a,b);
        }
    }
}
