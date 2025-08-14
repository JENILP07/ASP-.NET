using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class Program6
    {
        public void p6()
        {
            Console.WriteLine("Enter 1 for Celcius and 2 for Fahrenheit");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1) {
                Console.Write("Enter Temprature in f : ");
                double x = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine((x-32)/1.8);
            }
            else
            {
                Console.Write("Enter Temprature in c : ");
                double x = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine(x * 9/5 + 32);
            }
        }
    }
}
