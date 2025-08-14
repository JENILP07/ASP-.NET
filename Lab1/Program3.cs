using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class Program3
    {
     public void p3()
        {
            Console.Write("Enter Name : ");
            string name = Console.ReadLine();

            Console.Write("Enter Country Name : ");

            string country = Console.ReadLine();
            Console.WriteLine("Hello " + name + " from country " + country);
        }
    }
}
