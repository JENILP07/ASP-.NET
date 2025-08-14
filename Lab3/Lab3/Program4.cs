using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Program4
    {
        interface Calculate
        {

            void add(int a, int b);
            void sub(int a, int b);
        }
        class Result : Calculate
        {
            public void add(int a, int b)
            {
                Console.WriteLine("Addition");
                Console.WriteLine(a + b);
            }
            public void sub(int a, int b)
            {
                {
                    Console.WriteLine("Subtraction");
                    Console.WriteLine(a - b); ;
                }
            }

        }

        public void p4()
        {
            Console.Write("Enter a : ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter b : ");
            int b = Convert.ToInt32(Console.ReadLine());
            Result x1 = new Result();
            x1.add(a, b);
            x1.sub(a, b);
        }
    }
}
