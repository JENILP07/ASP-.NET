using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Program8
    {
        public void p8()
        {
            Console.WriteLine("Enter Operator (+, -, *, /):");
            string choice = Console.ReadLine();

            if (choice == "+")
            {
                Console.Write("Enter x: ");
                double x = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter y: ");
                double y = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Result: " + (x + y));
            }
            else if (choice == "-")
            {
                Console.Write("Enter x: ");
                double x = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter y: ");
                double y = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Result: " + (x - y));
            }
            else if (choice == "/")
            {
                Console.Write("Enter x: ");
                double x = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter y: ");
                double y = Convert.ToDouble(Console.ReadLine());
                if (y != 0)
                {
                    Console.WriteLine("Result: " + (x / y));
                }
                else
                {
                    Console.WriteLine("Error: Division by zero is not allowed.");
                }
            }
            else if (choice == "*")
            {
                Console.Write("Enter x: ");
                double x = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter y: ");
                double y = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Result: " + (x * y));
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
    }
}
